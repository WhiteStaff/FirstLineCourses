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

        private static void CheckFile()
        {
            try
            {
                if (File.Exists("data.xlsx")) return;
                if (MessageBox.Show("Файл отсутствует, скачать?", "Ошибка файла", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    FileLoader.Download("data.xlsx");
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
                        FileLoader.Download("data.xlsx");
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