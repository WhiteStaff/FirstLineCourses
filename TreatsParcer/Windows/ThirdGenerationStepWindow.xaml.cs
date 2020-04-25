using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ThreatsParser.Entities;

namespace ThreatsParser.Windows
{
    /// <summary>
    /// Логика взаимодействия для ThirdGenerationStepWindow.xaml
    /// </summary>
    public partial class ThirdGenerationStepWindow : Window
    {
        private GlobalPreferences _globalPreferences;

        public ThirdGenerationStepWindow(GlobalPreferences globalPreferences)
        {
            InitializeComponent();
            _globalPreferences = globalPreferences;

            _globalPreferences.Dangers = _globalPreferences.AllDangers
                .Where(x =>
                    _globalPreferences.Source
                        .Where(y => y.Item2)
                        .Select(y => y.Item1)
                        .Contains(x.Source) &&
                    _globalPreferences.Items
                        .Select(y => y.Name)
                        .Contains(x.ThreatName))
                .OrderBy(x => x.ThreatName)
                .ToList();

            var dataGridElements = new ObservableCollection<DangerousLevelLine>(_globalPreferences.Dangers);
            DangersGrid.ItemsSource = dataGridElements;
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _globalPreferences = new GlobalPreferences();
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            var win = new PreferencesWindow(_globalPreferences);
            win.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var win = new SecondGenerationStepWindow(_globalPreferences);
            win.Show();
            Close();
        }

        private void DangersGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
