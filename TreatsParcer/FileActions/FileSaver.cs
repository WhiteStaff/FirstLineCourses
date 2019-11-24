using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace ThreatsParser.FileActions
{
    static class FileSaver
    {
        public static void Save(List<Threat> items)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog {Filter = "Text file (*.txt)|*.txt"};
            string result = "";
            string separator =
                "---------------------------------------------------------------------------------------";
            items.ForEach(x => result += (x.ToString() + separator + "\n"));
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, result);
        }
    }
}