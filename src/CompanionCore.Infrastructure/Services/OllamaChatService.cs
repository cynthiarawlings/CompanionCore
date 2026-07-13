using System;
using System.Collections.Generic;
using System.Text;

using CompanionCore.Core.Interfaces;
using CompanionCore.Core.Models;

namespace CompanionCore.Infrastructure.Services;

public class OllamaChatService : IChatService
{
    public Task<ChatResponse> ChatAsync(ChatRequest request)
    {
        var response = new ChatResponse
        {
            Response = $"You said: {request.Message}"
        };

        return Task.FromResult(response);
    }
}
