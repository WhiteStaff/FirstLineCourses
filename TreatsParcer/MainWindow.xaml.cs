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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using TreatsParcer.Actions;
using TreatsParcer.Actions.Interfaces;

namespace TreatsParcer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ThreatInfo> _items;
        private int _pageNumber;
        private int _maxPages;

        public MainWindow() : this(new UIAction[] {new FileSaver()})
        {
            
        }

        private MainWindow(UIAction[] actions)
        {
            InitializeComponent();
            foreach (var item in actions.ToMenuItems())
            {
                MainMenu.Items.Add(item);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _items = new FileWorker().CheckFile();
        }


        private void TreatsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = TreatsGrid.SelectedItem as ThreatInfo;
            try
            {
                MessageBox.Show(x.ToString(), x.Name);
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Выберите столбец!", "Ошибка данных");
            }
        }

        private void All_OnLoaded(object sender, RoutedEventArgs e)
        {
            TreatsGrid.DataContext = _items.Where(x => x.Id <= 15);
            _pageNumber = 1;
            _maxPages = _items.Count / 15 + 1;
            PageInfo.Content = $"Страница {_pageNumber} из {_maxPages}";
            OneBack.Click += PageChanger_ButtonClick;
            MuchBack.Click += PageChanger_ButtonClick;
            OneNext.Click += PageChanger_ButtonClick;
            MuchNext.Click += PageChanger_ButtonClick;

            //TreatsGrid.Columns.
        }

        private void PageChanger_ButtonClick(object sender, RoutedEventArgs e)
        {
            TreatsGrid.DataContext = _items.Where(x => x.Id > (_pageNumber - 1) * 15 && x.Id <= (_pageNumber) * 15);
            PageInfo.Content = $"Страница {_pageNumber} из {_maxPages}";
        }

        private void MuchBack_OnClick(object sender, RoutedEventArgs e)
        {
            _pageNumber -= 10;
            if (_pageNumber < 1) _pageNumber = 1;
        }

        private void OneBack_Click(object sender, RoutedEventArgs e)
        {
            if (_pageNumber > 1) _pageNumber--;
        }

        private void OneNext_Click(object sender, RoutedEventArgs e)
        {
            if (_pageNumber < _maxPages) _pageNumber++;
        }

        private void MuchNext_Click(object sender, RoutedEventArgs e)
        {
            _pageNumber += 10;
            if (_pageNumber > _maxPages) _pageNumber = _maxPages;
        }
    }
}