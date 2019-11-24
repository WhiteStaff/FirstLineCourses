using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using OfficeOpenXml;
using ThreatsParser.Entities;

namespace ThreatsParser.FileActions
{
    static class FileUpdater
    {
        private static bool AreFilesEqual()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", "newdata.xlsx");
            }

            var file1 = File.ReadAllBytes("data.xlsx");
            var file2 = File.ReadAllBytes("newdata.xlsx");

            if (file1.Length != file2.Length) return false;
            return !file1.Where((t, i) => t != file2[i]).Any();
        }

        public static List<ThreatsChanges> GetDifference(List<Threat> items)
        {
            var result = new List<ThreatsChanges>();
            if (AreFilesEqual()) return result;

            byte[] bin = File.ReadAllBytes("newdata.xlsx");
                
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                
                int x = 0;
                var sheet = excelPackage.Workbook.Worksheets[1];
                var lines = Math.Min(sheet.Dimension.End.Row, items.Count+2);
                for (int i = 3; i <= lines; i++)
                {
                    var row = new string[8];
                    for (int j = 1; j <= 8; j++)
                    {
                        var value = sheet.Cells[i, j].Value.ToString();
                        row[j - 1] = value;
                    }

                    Threat currentNewThreat = new Threat(row);
                    var act = items[x];
                    var b = currentNewThreat.Equals(act);
                    if (!b)
                    {
                        result.Add(new ThreatsChanges(items[x], currentNewThreat));
                    }

                    x++;
                }
            }
            return result;
        }
    }
}