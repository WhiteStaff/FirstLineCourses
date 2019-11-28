using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using OfficeOpenXml;
using ThreatsParser.Entities;
using ThreatsParser.Exceptions;


namespace ThreatsParser.FileActions
{
    static class FileCreator
    {
        private static void Download()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://bdu.fstec.ru/documents/files/thrlist.xlsx", "data.xlsx");
                }
            }
            catch (Exception e)
            {
                throw new NoConnectionException();
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
            var excelData = new List<Threat>();
            try
            {
                CheckFile();
                try
                {
                    excelData = FileParser.Parse("data.xlsx");
                    
                }
                catch (Exception e)
                {
                    if (MessageBox.Show("Файл имеет неверный формат, возможно он поврежден\nСкачать файл заново?",
                            "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                    {
                        Download();
                        excelData = FileParser.Parse("data.xlsx");
                    }
                    else
                    {
                        throw new NoFileException();
                    }
                }
                return excelData;

            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}\nДальнейшая работа невозможна", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return excelData;
            }
            
        }
    }
}