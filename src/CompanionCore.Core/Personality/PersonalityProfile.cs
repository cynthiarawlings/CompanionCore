using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Personality
{
    public class PersonalityProfile
    {
        public string Summary { get; set; } = string.Empty;

        public string Traits { get; set; } = string.Empty;

        public string SpeakingStyle { get; set; } = string.Empty;

        public string SystemPrompt { get; set; } = string.Empty;
    }
}
