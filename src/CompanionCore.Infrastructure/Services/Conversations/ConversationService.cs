using CompanionCore.Core.Conversations;
using CompanionCore.Core.Interfaces;
using CompanionCore.Core.Models;
using CompanionCore.Infrastructure.Persistence;

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
                StartedAt = DateTime.UtcNow
            };


            _dbContext.Conversations.Add(conversation);

            await _dbContext.SaveChangesAsync();
        }


        var userMessage = new ConversationMessage
        {
            Id = Guid.NewGuid(),
            ConversationId = conversation.Id,
            Role = "user",
            Content = request.Message
        };


        _dbContext.ConversationMessages.Add(userMessage);


        await _dbContext.SaveChangesAsync();



        var response = await _chatService.ChatAsync(request);



        var assistantMessage = new ConversationMessage
        {
            Id = Guid.NewGuid(),
            ConversationId = conversation.Id,
            Role = "assistant",
            Content = response.Response
        };


        _dbContext.ConversationMessages.Add(assistantMessage);


        conversation.LastMessageAt = DateTime.UtcNow;


        await _dbContext.SaveChangesAsync();



        return response;
    }
}