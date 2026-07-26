using CompanionCore.Core.Conversations;
using CompanionCore.Core.Interfaces;
using CompanionCore.Core.Models;
using CompanionCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanionCore.Infrastructure.Services.Conversations;

public class ConversationService : IConversationService
{
    private readonly IChatService _chatService;
    private readonly CompanionDbContext _dbContext;

    public ConversationService(
        IChatService chatService,
        CompanionDbContext dbContext)
    {
        _chatService = chatService;
        _dbContext = dbContext;
    }

    public async Task<ChatResponse> ChatAsync(ChatRequest request)
    {
        // Load the existing conversation (if one was supplied).
        Conversation? conversation = null;

        if (request.ConversationId.HasValue)
        {
            conversation = await _dbContext.Conversations
                .FirstOrDefaultAsync(c =>
                    c.Id == request.ConversationId.Value &&
                    c.CompanionId == request.CompanionId);
        }

        // Create a new conversation if necessary.
        if (conversation == null)
        {
            conversation = new Conversation
            {
                Id = Guid.NewGuid(),
                CompanionId = request.CompanionId,
                StartedAt = DateTime.UtcNow,
                LastMessageAt = DateTime.UtcNow,

                // Use the user's first message as the default title.
                Title = request.Message.Length <= 40
                    ? request.Message
                    : request.Message[..40] + "..."
            };

            _dbContext.Conversations.Add(conversation);
        }

        // Load the selected companion.
        var companion = await _dbContext.Companions
            .FirstOrDefaultAsync(c => c.Id == request.CompanionId);

        if (companion is null)
        {
            throw new Exception("Companion not found.");
        }

        // Save the user's message.
        var userMessage = new ConversationMessage
        {
            Id = Guid.NewGuid(),
            ConversationId = conversation.Id,
            Role = "user",
            Content = request.Message,
            Timestamp = DateTime.UtcNow
        };

        _dbContext.ConversationMessages.Add(userMessage);

        // Save conversation + first message together.
        await _dbContext.SaveChangesAsync();

        // Build the messages sent to the AI.
        var messages = new List<ChatMessage>
        {
            new ChatMessage
            {
                Role = "system",
                Content = companion.SystemPrompt
            }
        };

        // Load conversation history.
        var history = await _dbContext.ConversationMessages
            .Where(m => m.ConversationId == conversation.Id)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();

        foreach (var message in history)
        {
            messages.Add(new ChatMessage
            {
                Role = message.Role,
                Content = message.Content
            });
        }

        // Ask the AI for a response.
        var response = await _chatService.ChatAsync(messages);

        // Save the assistant's reply.
        var assistantMessage = new ConversationMessage
        {
            Id = Guid.NewGuid(),
            ConversationId = conversation.Id,
            Role = "assistant",
            Content = response.Response,
            Timestamp = DateTime.UtcNow
        };

        _dbContext.ConversationMessages.Add(assistantMessage);

        conversation.LastMessageAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return new ChatResponse
        {
            ConversationId = conversation.Id,
            Response = response.Response
        };
    }
}