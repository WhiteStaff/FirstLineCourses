using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreatsParcer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TreatInfo> items;
        private bool isItemsExist = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            items = new FileWorker().CheckFile();
        }

        private void TreatsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = TreatsGrid.SelectedItem as TreatInfo;
            MessageBox.Show(x.ToString(), x.Name);
        }

        private void MuchBack_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void All_OnLoaded(object sender, RoutedEventArgs e)
        {
            TreatsGrid.DataContext = items.Where(x => x.Id <= 15);
            PageInfo.Content = $"1 of {items.Count / 15 + 1}";
        }
    }
}
