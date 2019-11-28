using System.Windows;
using ThreatsParser.MenuActions.Interfaces;

namespace ThreatsParser.MenuActions
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