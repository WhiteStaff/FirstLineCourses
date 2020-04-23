using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using ThreatsParser.FileActions;
using TreatsParser.Core;
using TreatsParser.Core.DataBase;

namespace ThreatsParser.Windows
{
    /// <summary>
    /// Логика взаимодействия для SecondGenerationStepWindow.xaml
    /// </summary>
    public partial class SecondGenerationStepWindow : Window
    {
        private int _pageNumber;
        private int _maxPages;
        private static ObservableCollection<Threat> _dataGridElements = new ObservableCollection<Threat>();
        private GlobalPreferences _globalPreferences;

        public SecondGenerationStepWindow(GlobalPreferences globalPreferences)
        {
            InitializeComponent();
            _globalPreferences = globalPreferences;

            if (globalPreferences.Items != null) return;

            using (var context = new ThreatDbContext())
            {
                if (context.Threat.Count() < 3)
                {
                    _globalPreferences.Items = FileCreator.GetParsedData();
                    context.Threat.Clear();
                    context.Source.Clear();
                    context.Target.Clear();
                    _globalPreferences.Items.ForEach(x => context.Threat.Add(x.ToDbModel()));
                    context.SaveChanges();
                }
                else
                {
                    _globalPreferences.Items = context.Threat.Include(x => x.Sources).Include(x => x.Targets)
                        .ToList()
                        .Select(x => x.ToEntity()).OrderBy(x => x.Id).ToList();
                }
            }
        }

        private void TreatsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = TreatsGrid.SelectedItem as Threat;
            try
            {
                MessageBox.Show(x.ToString(), $"УБИ.{x.Id}");
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Выберите столбец!", "Ошибка данных");
            }
        }

        private void All_OnLoaded(object sender, RoutedEventArgs e)
        {
            _dataGridElements = new ObservableCollection<Threat>(_globalPreferences.Items.Where(x => x.Id <= 15));
            TreatsGrid.DataContext = _dataGridElements;
            _pageNumber = 1;
            _maxPages = _globalPreferences.Items.Count / 15 + 1;
            PageInfo.Content = $"Страница {_pageNumber} из {_maxPages}";
            OneBack.Click += PageChanger_ButtonClick;
            MuchBack.Click += PageChanger_ButtonClick;
            OneNext.Click += PageChanger_ButtonClick;
            MuchNext.Click += PageChanger_ButtonClick;
        }

        private void PageChanger_ButtonClick(object sender, RoutedEventArgs e)
        {
            _dataGridElements =
                new ObservableCollection<Threat>(_globalPreferences.Items.Where(x =>
                    x.Id > (_pageNumber - 1) * 15 && x.Id <= (_pageNumber) * 15));
            TreatsGrid.DataContext = _dataGridElements;
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


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var first = new FirstGenerationStep(_globalPreferences);
            first.Show();
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            var third = new PreferencesWindow(_globalPreferences);
            third.Show();
            Close();
        }
    }
}