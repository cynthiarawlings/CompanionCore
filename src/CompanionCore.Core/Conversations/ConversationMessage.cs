using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Conversations;

public class ConversationMessage
{
    public Guid Id { get; set; }

    public Guid ConversationId { get; set; }

    public Conversation? Conversation { get; set; }

    public string Role { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}