using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Infrastructure.Models
{
    public class OllamaChatResponse
    {
        public OllamaMessage Message { get; set; } = new();
    }
}
