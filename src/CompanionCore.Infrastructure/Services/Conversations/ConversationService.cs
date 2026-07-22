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
        // Load the existing conversation or create a new one.
        var conversation = request.ConversationId.HasValue
            ? await _dbContext.Conversations
                .FindAsync(request.ConversationId.Value)
            : null;

        if (conversation == null)
        {
            conversation = new Conversation
            {
                Id = Guid.NewGuid(),
                CompanionId = request.CompanionId,
                StartedAt = DateTime.UtcNow,
                LastMessageAt = DateTime.UtcNow
            };

            _dbContext.Conversations.Add(conversation);

            await _dbContext.SaveChangesAsync();
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

        await _dbContext.SaveChangesAsync();

        // Build the messages that will be sent to the AI.
        var messages = new List<ChatMessage>
        {
            new ChatMessage
            {
                Role = "system",
                Content = companion.SystemPrompt
            }
        };

        // Load the conversation history.
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