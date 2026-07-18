using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Conversations;

public class Conversation
{
    public Guid Id { get; set; }

    public Guid CompanionId { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? LastMessageAt { get; set; }

    public string? Title { get; set; }

    public List<ConversationMessage> Messages { get; set; } = [];
}