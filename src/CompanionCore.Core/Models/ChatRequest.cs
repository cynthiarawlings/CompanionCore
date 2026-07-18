using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Models
{
    public class ChatRequest
    {
        public Guid CompanionId { get; set; }

        public Guid? ConversationId { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
