using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using ThreatsParser.Entities;

namespace ThreatsParser.FileActions
{
    static class FileParser
    {
        public static List<Threat> Parse(string path)
        {
            var excelData = new List<Threat>();
            byte[] bin = File.ReadAllBytes(path);
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                var sheet = excelPackage.Workbook.Worksheets[1];
                for (int i = 3; i <= sheet.Dimension.End.Row; i++)
                {
                    var row = new string[8];
                    for (int j = 1; j <= 8; j++)
                    {
                        var value = sheet.Cells[i, j].Value.ToString();
                        row[j - 1] = value;
                    }

                    excelData.Add(new Threat(row));
                }
            }


            return excelData;
        }
    }
}