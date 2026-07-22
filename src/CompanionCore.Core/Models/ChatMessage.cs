using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Models
{
    public class ChatMessage
    {
        public string Role { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}
