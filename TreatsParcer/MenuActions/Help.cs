using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThreatsParser.Actions.Interfaces;

namespace ThreatsParser.Actions
{
    class Help : MenuAction
    {
        public string Category => "About";
        public string Name => "Помощь";

        public void Perform()
        {
            MessageBox.Show(
                " - Для получения полной информации об угрозе, сделайте двойной клик мыши по интересующей угрозе в списке угроз.\n " +
                "- Для проверки обновлений базы угроз нажмите кнопку \"" +
                "Проверить обновления\"\n - Для сохранения базы в текстовом формате нажмите кнопку \"Сохранить\"\n",
                Name);
        }
    }
}