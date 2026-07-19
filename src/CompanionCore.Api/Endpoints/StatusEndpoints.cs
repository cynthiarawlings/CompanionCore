using Microsoft.EntityFrameworkCore;
using CompanionCore.Infrastructure.Persistence;

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

        app.MapGet("/api/status/db", async (CompanionDbContext db) =>
        {
            var connection = db.Database.GetDbConnection();

            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT SUSER_SNAME()";

            var result = await command.ExecuteScalarAsync();

            return Results.Ok(new
            {
                sqlLogin = result
            });
        });
    }
}
