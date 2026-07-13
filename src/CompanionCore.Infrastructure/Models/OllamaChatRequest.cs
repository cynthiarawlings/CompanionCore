using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Infrastructure.Models
{
    public class OllamaChatRequest
    {
        public string Model { get; set; } = string.Empty;

        public List<OllamaMessage> Messages { get; set; } = new();

        public bool Stream { get; set; }
    }
}
