using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using OfficeOpenXml;


namespace TreatsParcer
{
    class FileWorker
    {
        private  void Download()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", "data.xlsx");
            }
        }

        public List<ThreatInfo> CheckFile()
        {
            try
            {
                if (!File.Exists("data.xlsx"))  Download();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return Parse();
        }

        public List<ThreatInfo> Parse()
        {
            var excelData = new List<ThreatInfo>();
            byte[] bin = File.ReadAllBytes("data.xlsx");
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                int x = 0;
                var sheet = excelPackage.Workbook.Worksheets[1];
                    for (int i = 3; i <= sheet.Dimension.End.Row; i++)
                    {
                        x++;
                        var row = new string[8];
                        for (int j = 1; j <= 8; j++)
                        {
                            var value = sheet.Cells[i, j].Value.ToString();
                            row[j - 1] = value;
                        }
                        excelData.Add(new ThreatInfo(row));
                    }
            }

            return excelData;
        }
    }
}
