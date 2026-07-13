using CompanionCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Interfaces
{
    public interface IChatService
    {
        Task<ChatResponse> ChatAsync(ChatRequest request);
    }
}
