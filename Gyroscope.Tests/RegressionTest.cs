namespace Gyroscope.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Golden-master regression test guarding the simulation's numerical output.
    ///
    /// The concern when moving from .NET Framework (net48) to modern .NET (net8.0)
    /// is that the simulation output could silently change because of two things:
    ///   1. System.Random — a *seeded* Random uses the same legacy algorithm on
    ///      both runtimes (only the parameterless ctor diverges in .NET 6+), and
    ///   2. floating-point / JIT behaviour.
    ///
    /// This test runs a fully deterministic simulation (fixed seed) and fingerprints
    /// every output row by hashing the raw IEEE-754 bits of the float fields (not
    /// their text form, which formats differently across runtimes). The same golden
    /// values must hold on net48 and net8.0 — proving the migration is output-safe.
    /// </summary>
    [TestClass]
    public class RegressionTest
    {
        // Fixed scenario. Any change to these invalidates the golden values below.
        private const int Seed = 123456;
        private const int Duration = 40;

        // Golden values captured on the original runtime, .NET Framework 4.8.9337.0.
        // The same values are asserted on net8.0, proving the seeded run is
        // bit-identical across runtimes (RNG sequence and float results).
        private const int GoldenRowCount = 300;
        private const string GoldenHash = "ab9a6b2185ebdc4c7e42224d916e2378da0217d0974a7df920551838a3cf8f38";

        [TestMethod]
        public void Simulation_WithFixedSeed_ProducesGoldenOutput()
        {
            RecordingOutput output = RunDeterministicSimulation(Seed, Duration);

            string hash = Fingerprint(output.Rows);

            Console.WriteLine($"[Regression] runtime   = {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"[Regression] rowCount  = {output.Rows.Count}");
            Console.WriteLine($"[Regression] hash      = {hash}");

            Assert.AreEqual(GoldenRowCount, output.Rows.Count, "Output row count changed.");
            Assert.AreEqual(GoldenHash, hash, "Output fingerprint changed — the numerical output is no longer bit-identical.");
        }

        /// <summary>
        /// Builds a deterministic model with a fixed seed, runs it to completion on
        /// its background thread, and returns the collected output rows.
        /// </summary>
        private static RecordingOutput RunDeterministicSimulation(int seed, int duration)
        {
            ParasiteFactory parasiteFactory = new ParasiteFactory(
                constitution: 10f,
                resources: 0f,
                reproductionReq: 10f);

            HostFactory hostFactory = new HostFactory(
                minimalResourceGain: 0.5f,
                normalResourceGain: 1f,
                immunityDamage: 1f,
                immunoDecay: 10f,
                immunoCompitence: 50f,
                rows: 10,
                columns: 10);

            SimFactory simFactory = new SimFactory(
                initialParasiteQty: 2,
                parasiteTravelDist: 1f,
                parasiteFactory: parasiteFactory,
                hostLength: 10f,
                hostWidth: 10f,
                hostFactory: hostFactory);

            Model model = new Model(1, simFactory, seed);

            RecordingOutput output = new RecordingOutput();

            // Model.Run executes on a background thread; wait for the STOPPED transition.
            ManualResetEventSlim finished = new ManualResetEventSlim(false);
            model.Status.ExecutionEvent += (s, e) =>
            {
                if (e.CurrentState == Execution.State.STOPPED)
                {
                    finished.Set();
                }
            };

            model.Run(duration, output);

            if (!finished.Wait(TimeSpan.FromSeconds(60)))
            {
                throw new TimeoutException("Simulation did not complete within 60s.");
            }

            return output;
        }

        /// <summary>
        /// Hashes the output rows by their exact numeric content: integer fields as
        /// bytes and float fields as their raw IEEE-754 bit pattern. This compares the
        /// computed values bit-for-bit and is immune to float-to-string formatting
        /// differences between runtimes.
        /// </summary>
        private static string Fingerprint(IList<OutputRow> rows)
        {
            using (MemoryStream buffer = new MemoryStream())
            {
                foreach (OutputRow row in rows)
                {
                    buffer.Write(BitConverter.GetBytes(row.Simulation), 0, 4);
                    buffer.Write(BitConverter.GetBytes(row.Time), 0, 4);
                    buffer.Write(BitConverter.GetBytes(row.Parasite), 0, 4);
                    buffer.Write(BitConverter.GetBytes(row.X), 0, 4);
                    buffer.Write(BitConverter.GetBytes(row.Y), 0, 4);
                    buffer.Write(BitConverter.GetBytes(row.Resources), 0, 4);
                    buffer.Write(BitConverter.GetBytes(row.Constitution), 0, 4);
                }

                using (SHA256 sha = SHA256.Create())
                {
                    byte[] digest = sha.ComputeHash(buffer.ToArray());
                    StringBuilder hex = new StringBuilder(digest.Length * 2);
                    foreach (byte b in digest)
                    {
                        hex.Append(b.ToString("x2", CultureInfo.InvariantCulture));
                    }

                    return hex.ToString();
                }
            }
        }

        /// <summary>A single captured output row.</summary>
        private struct OutputRow
        {
            public int Simulation;
            public int Time;
            public int Parasite;
            public float X;
            public float Y;
            public float Resources;
            public float Constitution;
        }

        /// <summary>Collects the simulation output in arrival order.</summary>
        private sealed class RecordingOutput : ISimulationOutput
        {
            public readonly List<OutputRow> Rows = new List<OutputRow>();

            public void Add(int simulation, int time, int parasite, float xPosition, float yPosition, float resources, float constitution)
            {
                Rows.Add(new OutputRow
                {
                    Simulation = simulation,
                    Time = time,
                    Parasite = parasite,
                    X = xPosition,
                    Y = yPosition,
                    Resources = resources,
                    Constitution = constitution,
                });
            }
        }
    }
}
