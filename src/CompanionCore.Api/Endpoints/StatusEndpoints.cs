namespace CompanionCore.Api.Endpoints;

public static class StatusEndpoints
{
    public static void MapStatusEndpoints(this WebApplication app)
    {
        app.MapGet("/api/status", () =>
        {
            return Results.Ok(new
            {
                application = "CompanionCore",
                version = "1.0.0",
                status = "Healthy"
            });
        });
    }
}
