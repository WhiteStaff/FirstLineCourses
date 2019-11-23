using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatsParcer.Actions.Interfaces
{
    public interface UIAction
    {
        string Category { get; }
        string Name { get; }
        void Perform();
    }
}
