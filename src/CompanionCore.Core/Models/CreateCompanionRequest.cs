using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Models;

public class CreateCompanionRequest
{
    public string Name { get; set; } = string.Empty;

    public string Universe { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PersonalitySummary { get; set; } = string.Empty;

    public string SpeakingStyle { get; set; } = string.Empty;

    public string SystemPrompt { get; set; } = string.Empty;
}