using CompanionCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanionCore.Api.Endpoints;

public static class ConversationEndpoints
{
    public static void MapConversationEndpoints(
        this WebApplication app)
    {
        // Get a single conversation with all messages
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

            return Results.Ok(new
            {
                conversation.Id,
                conversation.CompanionId,
                conversation.StartedAt,
                conversation.LastMessageAt,
                conversation.Title,
                Messages = conversation.Messages
                    .OrderBy(m => m.Timestamp)
                    .Select(m => new
                    {
                        m.Id,
                        m.Role,
                        m.Content,
                        m.Timestamp
                    })
            });
        });

        // Get all conversations for one companion
        app.MapGet("/api/companions/{companionId:guid}/conversations", async (
            Guid companionId,
            CompanionDbContext db) =>
        {
            var conversations = await db.Conversations
                .Where(c => c.CompanionId == companionId)
                .OrderByDescending(c => c.LastMessageAt)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    c.StartedAt,
                    c.LastMessageAt
                })
                .ToListAsync();

            return Results.Ok(conversations);
        });

        // Rename a conversation
        app.MapPut("/api/conversations/{id:guid}", async (
            Guid id,
            RenameConversationRequest request,
            CompanionDbContext db) =>
        {
            var conversation = await db.Conversations
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conversation is null)
            {
                return Results.NotFound();
            }

            conversation.Title = request.Title;

            await db.SaveChangesAsync();

            return Results.Ok();
        });

        // Delete a conversation
        app.MapDelete("/api/conversations/{id:guid}", async (
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

            db.ConversationMessages.RemoveRange(conversation.Messages);
            db.Conversations.Remove(conversation);

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }

    public class RenameConversationRequest
    {
        public string Title { get; set; } = string.Empty;
    }
}