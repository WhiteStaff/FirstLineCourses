using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ThreatsParser;
using ThreatsParser.Entities;
using ThreatsParser.FileActions;
using ThreatsParser.MenuActions;
using ThreatsParser.MenuActions.Interfaces;
using ThreatsParser.Windows;
using TreatsParser.Core;
using TreatsParser.Core.DataBase;
using TreatsParser.Core.DataBase.Models;
using TreatsParser.Core.Helpers;

namespace ThreatsParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GlobalPreferences _globalPreferences;

        public MainWindow() : this(new IMenuAction[] {new About(), new Help()})
        {
        }

        private MainWindow(IMenuAction[] actions)
        {
            InitializeComponent();
            _globalPreferences = FileCreator.GetParsedData();

            foreach (var item in actions.ToMenuItems())
            {
                MainMenu.Items.Add(item);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //FileSaver.Save(_items);
        }

        private void CheckUpdates_OnClick(object sender, RoutedEventArgs e)
        {
            var updateWindow = new UpdatesResultWindow();
            /*try
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
            }*/
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new FirstGenerationStep(_globalPreferences).Show();
        }
    }
}