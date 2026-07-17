using System;
using System.Collections.Generic;
using System.Text;
using CompanionCore.Core.Models;

namespace CompanionCore.Core.Interfaces;

public interface IConversationService
{
    Task<ChatResponse> ChatAsync(ChatRequest request);
}