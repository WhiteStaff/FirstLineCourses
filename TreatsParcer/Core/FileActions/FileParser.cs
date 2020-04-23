using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using OfficeOpenXml;
using ThreatsParser.Entities;
using ThreatsParser.Exceptions;

namespace ThreatsParser.FileActions
{
    static class FileParser
    {
        public static List<Threat> Parse(string path)
        {
            Download("data.xlsx");
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

            File.Delete(path);

            return excelData;
        }

        private static void Download(string name)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://bdu.fstec.ru/files/documents/thrlist.xlsx", name);
                }
            }
            catch (Exception e)
            {
                throw new NoConnectionException();
            }

        }
    }
}