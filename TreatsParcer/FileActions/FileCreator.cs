using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using OfficeOpenXml;
using TreatsParcer.Exceptions;


namespace ThreatsParser.FileActions
{
    class FileCreator
    {
        private static void Download()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", "data.xlsx");
            }
        }

        private static void CheckFile()
        {
            try
            {
                if (File.Exists("data.xlsx")) return;
                if (MessageBox.Show("Файл отсутствует, скачать?", "Ошибка файла", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    Download();
                else throw  new NoFileException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Threat> GetParsedData()
        {
            try
            {
                CheckFile();
                return Parse();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}\nДальнейшая работа невозможна", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return new List<Threat>();
            }
            
            
        }

        private static List<Threat> Parse()
        {
            var excelData = new List<Threat>();
            byte[] bin = File.ReadAllBytes("data.xlsx");
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                try
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
                catch (Exception e)
                {
                    if (MessageBox.Show("Файл имеет неверный формат, возможно он поврежден\nСкачать файл заново?",
                            "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                    {
                        Download();
                        excelData = Parse();
                    }
                    else
                    {
                        throw new NoFileException();
                    }
                }
            }

            return excelData;
        }
    }
}