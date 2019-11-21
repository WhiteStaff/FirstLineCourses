using System;
using System.Collections;
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

        public ArrayList CheckFile()
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

        public ArrayList Parse()
        {
            var excelData = new ArrayList();
            byte[] bin = File.ReadAllBytes("data.xlsx");
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                int x = 0;
                var sheet = excelPackage.Workbook.Worksheets[1];
                //MessageBox.Show(sheet.Cells[3, 2].Value.ToString());
                    //loop all rows
                    for (int i = 3; i <= sheet.Dimension.End.Row; i++)
                    {
                        x++;
                        var row = new string[8];
                        for (int j = 1; j <= 8; j++)
                        {
                            var value = sheet.Cells[i, j].Value.ToString();
                            row[j - 1] = value;
                        }
                        excelData.Add(new TreatInfo(row));
                    }
            }

            return excelData;
        }
    }
}
