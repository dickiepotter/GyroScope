using BioSimWeb;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Runs the unchanged simulation engine for the supplied parameters and returns
// every timestep's parasite positions/state for the browser to animate.
app.MapPost("/api/run", (SimParameters parameters) =>
{
    try
    {
        return Results.Json(SimRunner.Run(parameters));
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (TimeoutException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();
