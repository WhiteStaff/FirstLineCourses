using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ThreatsParser;
using ThreatsParser.Entities;
using ThreatsParser.FileActions;
using ThreatsParser.MenuActions;
using ThreatsParser.MenuActions.Interfaces;

namespace ThreatsParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Threat> _items;
        private int _pageNumber;
        private int _maxPages;

        public MainWindow() : this(new IMenuAction[] {new About(), new Help()})
        {
        }

        private MainWindow(IMenuAction[] actions)
        {
            InitializeComponent();

            foreach (var item in actions.ToMenuItems())
            {
                MainMenu.Items.Add(item);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _items = FileCreator.GetParsedData();
        }


        private void TreatsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var x = TreatsGrid.SelectedItem as Threat;
            try
            {
                MessageBox.Show(x.ToString(), $"УБИ.{x.Id.ToString()}");
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FileSaver.Save(_items);
        }

        private void CheckUpdates_OnClick(object sender, RoutedEventArgs e)
        {
            var updateWindow = new UpdatesResultWindow();
            try
            {
                List<ThreatsChanges> changes = FileUpdater.GetDifference(_items, out var newItems);
                if (changes.Count == 0)
                {
                    File.Delete("newdata.xlsx");
                    MessageBox.Show("Используется актуальная версия файла!", "Успешно", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    updateWindow.Close();
                }
                else
                {
                    File.Delete("data.xlsx");
                    File.Move("newdata.xlsx", "data.xlsx");
                    _items = newItems;
                    TreatsGrid.DataContext =
                        _items.Where(x => x.Id > (_pageNumber - 1) * 15 && x.Id <= (_pageNumber) * 15);
                    updateWindow.Changes.ItemsSource = changes;
                    updateWindow.Info.Text = $"Идентификаторы угроз изменившихся записей (Всего {changes.Count}):";
                    updateWindow.Show();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка обновления!\n{exception.Message}", "Ошибка");
                updateWindow.Close();
            }
        }
    }
}