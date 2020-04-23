using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace ThreatsParser.Entities
{
    public class GlobalPreferences
    {
        public List<Threat> Items { get; set; }

        public List<(string, bool)> Source { get; set; }

        public List<(string, bool)> Targets { get; set; }

        public InitialSecurityLevel InitialSecurityLevel { get; set; }
    }
}