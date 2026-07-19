using CompanionCore.Core.Companions;
using CompanionCore.Core.Models;
using CompanionCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanionCore.Api.Endpoints;

public static class CompanionEndpoints
{
    public static void MapCompanionEndpoints(this WebApplication app)
    {
        app.MapGet("/api/companions", async (
            CompanionDbContext db) =>
        {
            var companions = await db.Companions
                .ToListAsync();

            return Results.Ok(companions);
        });


        app.MapPost("/api/companions", async (
            CreateCompanionRequest request,
            CompanionDbContext db) =>
        {
            var companion = new Companion
            {
                Id = Guid.NewGuid(),

                Name = request.Name,

                Universe = request.Universe,

                Description = request.Description,

                PersonalitySummary = request.PersonalitySummary,

                SpeakingStyle = request.SpeakingStyle,

                SystemPrompt = request.SystemPrompt
            };


            db.Companions.Add(companion);

            await db.SaveChangesAsync();


            return Results.Created(
                $"/api/companions/{companion.Id}",
                companion);
        });

        app.MapGet("/api/companions/{id:guid}", async (
            Guid id,
            CompanionDbContext db) =>
        {
        var companion = await db.Companions
            .FirstOrDefaultAsync(c => c.Id == id);

        if (companion is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(companion);
        });

        app.MapPut("/api/companions/{id:guid}", async (
            Guid id,
            UpdateCompanionRequest request,
            CompanionDbContext db) =>
        {
            var companion = await db.Companions
                .FirstOrDefaultAsync(c => c.Id == id);

            if (companion is null)
            {
                return Results.NotFound();
            }


            companion.Name = request.Name;
            companion.Universe = request.Universe;
            companion.Description = request.Description;
            companion.PersonalitySummary = request.PersonalitySummary;
            companion.SpeakingStyle = request.SpeakingStyle;
            companion.SystemPrompt = request.SystemPrompt;


            await db.SaveChangesAsync();


            return Results.Ok(companion);
        });


        app.MapDelete("/api/companions/{id:guid}", async (
            Guid id,
            CompanionDbContext db) =>
        {
            var companion = await db.Companions
                .FirstOrDefaultAsync(c => c.Id == id);

            if (companion is null)
            {
                return Results.NotFound();
            }


            db.Companions.Remove(companion);

            await db.SaveChangesAsync();


            return Results.NoContent();
        });

    }
}