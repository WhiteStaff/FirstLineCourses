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
        public static  List<Threat> GetParsedData()
        {
            var excelData = new List<Threat>();
            try
            {
                excelData = FileParser.Parse("data.xlsx");
                return excelData;
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.StackTrace}\nДальнейшая работа невозможна", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
                return excelData;
            }
        }
    }
}