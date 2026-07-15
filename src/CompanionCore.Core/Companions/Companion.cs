using CompanionCore.Core.Personality;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanionCore.Core.Companions;

public class Companion
{
    public CompanionIdentity Identity { get; set; } = new();

    public PersonalityProfile Personality { get; set; } = new();
}