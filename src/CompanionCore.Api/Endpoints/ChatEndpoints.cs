using CompanionCore.Core.Interfaces;
using CompanionCore.Core.Models;

namespace CompanionCore.Api.Endpoints;

public static class ChatEndpoints
{
    public static void MapChatEndpoints(this WebApplication app)
    {
        app.MapPost("/api/chat", async (
            ChatRequest request,
            IConversationService conversationService) =>
        {
            var response = await conversationService.ChatAsync(request);

            return Results.Ok(response);
        });
    }
}