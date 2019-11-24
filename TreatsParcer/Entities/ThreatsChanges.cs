using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreatsParser;

namespace ThreatsParser.Entities
{
    class ThreatsChanges
    {
        private readonly Threat _was;
        private readonly Threat _will;
        public string[] Was => _was == null ? new [] {"", "", "", "", "", "", "", ""} : _was.GetValuesAsArray();
        public string[] Will => _was == null ? new[] { "", "", "", "", "", "", "", "" } : _was.GetValuesAsArray();
        public ThreatsChanges(Threat was, Threat will)
        {
            _was = was;
            _will = will;
        }

        public override string ToString()
        {
            return _was == null ? $"УБИ.{_was.Id}" : $"УБИ.{_will.Id}";
        }
    }
}
