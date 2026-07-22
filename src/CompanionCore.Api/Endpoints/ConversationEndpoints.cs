using CompanionCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanionCore.Api.Endpoints;

public static class ConversationEndpoints
{
    public static void MapConversationEndpoints(
        this WebApplication app)
    {
        app.MapGet("/api/conversations/{id:guid}", async (
            Guid id,
            CompanionDbContext db) =>
        {
            var conversation = await db.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conversation is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(conversation);
        });


        app.MapDelete("/api/conversations/{id:guid}", async (
            Guid id,
            CompanionDbContext db) =>
        {
            var conversation = await db.Conversations
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conversation is null)
            {
                return Results.NotFound();
            }

            db.Conversations.Remove(conversation);

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}