using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThreatsParser.Entities;

namespace ThreatsParser
{
    /// <summary>
    /// Логика взаимодействия для UpdatesResultWindow.xaml
    /// </summary>
    public partial class UpdatesResultWindow : Window
    {
        private string[] names;

        public UpdatesResultWindow()
        {
            InitializeComponent();

            names = new[]
            {
                "Идентификатор угрозы", "Название угрозы", "Описание", "Источник",
                "Объект воздействия", "Нарушение конфиденциальности", "Нарушение целостности",
                "Нарушение доступности"
            };
            Changes.SelectionChanged += Changes_OnSelected;
        }

        private void Changes_OnSelected(object sender, RoutedEventArgs e)
        {
            Changelog.Visibility = Visibility.Visible;
            Changelog.Items.Clear();
            var selectedItem = Changes.SelectedItem as ThreatsChanges;
            var first = selectedItem.Was;
            var second = selectedItem.Will;
            for (var i = 1; i < 8; i++)
            {
                Changelog.Items.Add(new[] {names[i], first[i], second[i]});
            }
        }
    }
}