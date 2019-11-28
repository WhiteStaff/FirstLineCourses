using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml.Schema;
using ThreatsParser.Actions.Interfaces;

namespace ThreatsParser.Actions
{
    class About : IMenuAction
    {
        public string Category => "About";
        public string Name => "О программе";

        private const string CurrentVersion = "1.3";

        public void Perform()
        {
            MessageBox.Show($"Программа для парсинга базы угроз РФ. \nВерсия {CurrentVersion} \n" +
                            "Внимание!! Поддержа программы заканчивается 31.12.2019", Name);
        }
    }
}