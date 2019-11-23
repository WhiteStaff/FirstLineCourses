using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TreatsParcer.Actions;
using TreatsParcer.Actions.Interfaces;

namespace TreatsParcer.Actions
{
    class FileSaver : UIAction
    {
        public string Category => "File";
        public string Name => "Save";
        public void Perform()
        {
            MessageBox.Show("11110");
        }
    }
}
