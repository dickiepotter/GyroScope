using System.Threading;
using HostParasiteSim;

namespace BioSimWeb;

/// <summary>
/// The simulation parameters, mirroring the inputs of the desktop UI. These map
/// directly onto the engine's HostFactory / ParasiteFactory / SimFactory / Model
/// constructors — the web app builds the model exactly as BioSimUI does.
/// </summary>
public class SimParameters
{
    public float HostWidth { get; set; } = 10f;
    public float HostLength { get; set; } = 10f;
    public int Rows { get; set; } = 10;
    public int Columns { get; set; } = 10;

    public float MinResourceGain { get; set; } = 0.5f;
    public float NormalResourceGain { get; set; } = 1f;
    public float ImmunityDamage { get; set; } = 1f;
    public float ImmunoDecay { get; set; } = 10f;
    public float ImmunoCompetence { get; set; } = 50f;

    public float Constitution { get; set; } = 10f;
    public float InitialResources { get; set; } = 0f;
    public float ReproductionReq { get; set; } = 10f;
    public float MoveDistance { get; set; } = 1f;

    public int InitialParasites { get; set; } = 2;
    public int Repetitions { get; set; } = 1;
    public int Duration { get; set; } = 40;

    /// <summary>Random seed; a negative value uses a non-deterministic seed.</summary>
    public int Seed { get; set; } = 12345;
}

public record ParasiteDto(int Id, double X, double Y, double R, double C);
public record FrameDto(int T, List<ParasiteDto> P);
public record SimResult(
    double Width,
    double Length,
    int Rows,
    int Cols,
    int Seed,
    double MaxConstitution,
    int FrameCount,
    bool Truncated,
    List<FrameDto> Frames);

/// <summary>
/// Captures the engine's per-parasite, per-timestep output. This is the same
/// ISimulationOutput contract the regression test and the desktop UI use, so the
/// data shown in the browser is exactly what the engine produces.
/// </summary>
internal sealed class FrameCollector : ISimulationOutput
{
    private readonly int maxRows;
    private readonly List<(int Sim, int Time, int Parasite, float X, float Y, float Res, float Con)> rows = new();

    public bool Truncated { get; private set; }

    public FrameCollector(int maxRows) => this.maxRows = maxRows;

    public void Add(int simulation, int time, int parasite, float xPosition, float yPosition, float resources, float constitution)
    {
        if (this.rows.Count >= this.maxRows)
        {
            this.Truncated = true;
            return;
        }

        this.rows.Add((simulation, time, parasite, xPosition, yPosition, resources, constitution));
    }

    public SimResult ToResult(SimParameters p)
    {
        var byTime = new SortedDictionary<int, List<ParasiteDto>>();

        foreach (var r in this.rows)
        {
            if (!byTime.TryGetValue(r.Time, out var list))
            {
                list = new List<ParasiteDto>();
                byTime[r.Time] = list;
            }

            list.Add(new ParasiteDto(r.Parasite, Round(r.X), Round(r.Y), Round(r.Res), Round(r.Con)));
        }

        var frames = byTime.Select(kv => new FrameDto(kv.Key, kv.Value)).ToList();

        return new SimResult(
            p.HostWidth,
            p.HostLength,
            p.Rows,
            p.Columns,
            p.Seed,
            p.Constitution,
            frames.Count,
            this.Truncated,
            frames);
    }

    private static double Round(float value) => Math.Round(value, 3);
}

/// <summary>
/// Runs the unchanged simulation engine and returns the captured frames.
/// </summary>
public static class SimRunner
{
    /// <summary>Hard caps so a runaway (exponentially reproducing) run cannot exhaust memory or hang.</summary>
    private const int MaxDuration = 500;
    private const int MaxInitialParasites = 200;
    private const int MaxRepetitions = 20;
    private const int MaxRows = 300_000;
    private static readonly TimeSpan RunTimeout = TimeSpan.FromSeconds(30);

    public static SimResult Run(SimParameters p)
    {
        Validate(p);

        // Build the model exactly as BioSimUI.runBtn_Click does.
        var hostFactory = new HostFactory(
            p.MinResourceGain,
            p.NormalResourceGain,
            p.ImmunityDamage,
            p.ImmunoDecay,
            p.ImmunoCompetence,
            p.Rows,
            p.Columns);

        var parasiteFactory = new ParasiteFactory(p.Constitution, p.InitialResources, p.ReproductionReq);

        var simFactory = new SimFactory(
            p.InitialParasites,
            p.MoveDistance,
            parasiteFactory,
            p.HostLength,
            p.HostWidth,
            hostFactory);

        var model = new Model(p.Repetitions, simFactory, p.Seed);

        var collector = new FrameCollector(MaxRows);
        using var finished = new ManualResetEventSlim(false);

        model.Status.ExecutionEvent += (_, e) =>
        {
            if (e.CurrentState == Execution.State.STOPPED)
            {
                finished.Set();
            }
        };

        // Run(duration, output) drives the simulation on a background thread and
        // reports every parasite at every timestep through the collector.
        model.Run(p.Duration, collector);

        if (!finished.Wait(RunTimeout))
        {
            model.ImmediateHalt();
            throw new TimeoutException(
                "The simulation did not finish within the time limit. Try a shorter duration or fewer/slower-reproducing parasites.");
        }

        return collector.ToResult(p);
    }

    private static void Validate(SimParameters p)
    {
        Require(p.HostWidth > 0 && p.HostLength > 0, "Host width and length must be greater than 0.");
        Require(p.Rows >= 1 && p.Columns >= 1, "Rows and columns must be at least 1.");
        Require(p.MoveDistance >= 0, "Move distance must not be negative.");
        Require(p.Constitution > 0, "Constitution must be greater than 0.");
        Require(p.ReproductionReq > 0, "Reproduction requirement must be greater than 0.");
        Require(p.InitialParasites >= 1 && p.InitialParasites <= MaxInitialParasites,
            $"Initial parasites must be between 1 and {MaxInitialParasites}.");
        Require(p.Repetitions >= 1 && p.Repetitions <= MaxRepetitions,
            $"Repetitions must be between 1 and {MaxRepetitions}.");
        Require(p.Duration >= 1 && p.Duration <= MaxDuration,
            $"Duration must be between 1 and {MaxDuration} timesteps.");
    }

    private static void Require(bool condition, string message)
    {
        if (!condition)
        {
            throw new ArgumentException(message);
        }
    }
}
