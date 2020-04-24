using System.IO.Packaging;
using ThreatsParser.Entities.Enums;

namespace ThreatsParser.Entities
{
    public class DangerousLevelLine
    {
        public string Source { get; }

        public string Target { get; }

        public DangerLevel DangerLevel { get; set; }

        public DangerousLevelLine(string source, string target)
        {
            Source = source;
            Target = target;
        }

        public bool Equal(string target, string source)
        {
            return Target == target && Source == source;
        }
    }
}