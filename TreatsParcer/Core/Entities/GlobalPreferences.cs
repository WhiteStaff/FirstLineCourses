using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using ThreatsParser.Entities.Enums;

namespace ThreatsParser.Entities
{
    public class GlobalPreferences
    {
        public List<Threat> Items { get; set; }

        public List<(string, bool)> Source { get; set; }

        public List<(string, bool)> Targets { get; set; }

        public InitialSecurityLevel InitialSecurityLevel { get; set; }

        public List<DangerousLevelLine> Dangers { get; set; }

        public GlobalPreferences()
        {
            Items = new List<Threat>();
            Source = new List<(string, bool)>();
            Targets = new List<(string, bool)>();
            Dangers = new List<DangerousLevelLine>();
        }
    }
}