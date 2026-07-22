using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Models
{
    public class ChatResponse
    {
        public Guid ConversationId { get; set; }

        public string Response { get; set; } = string.Empty;
    }
}
