using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using TreatsParcer.Actions;
using TreatsParcer.Actions.Interfaces;

namespace TreatsParcer.FileActions
{
    static class FileSaver
    {
        public static void Save(List<ThreatInfo> items)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            string result = "";
            string separator =
                "---------------------------------------------------------------------------------------";
            items.ForEach(x => result += (x.ToString() + separator + "\n"));
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, result);
        }
    }
}