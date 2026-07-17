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
        return await _chatService.ChatAsync(request);
    }
}