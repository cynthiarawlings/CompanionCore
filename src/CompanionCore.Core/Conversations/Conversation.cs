using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Conversations;

public class Conversation
{
    public Guid Id { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public List<ConversationMessage> Messages { get; set; } = [];
}
