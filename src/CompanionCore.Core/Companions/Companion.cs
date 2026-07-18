using CompanionCore.Core.Personality;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Companions;

public class Companion
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Universe { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PersonalitySummary { get; set; } = string.Empty;

    public string SpeakingStyle { get; set; } = string.Empty;

    public string SystemPrompt { get; set; } = string.Empty;
}