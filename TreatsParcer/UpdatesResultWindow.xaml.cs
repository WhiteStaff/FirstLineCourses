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
        public UpdatesResultWindow()
        {
            InitializeComponent();
        }

        private void Changes_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = Changes.SelectedItem as ThreatsChanges;
            for (int i = 0; i < 8; i++)
            {
                Changelog.Items.
            }
           

            
        }
    }
}
