using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using OfficeOpenXml.Packaging.Ionic.Zip;
using ThreatsParser.Entities;

namespace ThreatsParser.Windows
{
    /// <summary>
    /// Логика взаимодействия для ThirdGenerationStepWindow.xaml
    /// </summary>
    public partial class ThirdGenerationStepWindow : Window
    {
        private GlobalPreferences _globalPreferences;
        private static ObservableCollection<DangerousLevelLine> _dataGridElements;

        public ThirdGenerationStepWindow(GlobalPreferences globalPreferences)
        {
            InitializeComponent();
            _globalPreferences = globalPreferences;

            _globalPreferences.Dangers = _globalPreferences.Dangers
                .Where(x => 
                    _globalPreferences.Source
                        .Where(y => y.Item2)
                        .Select(y => y.Item1)
                        .Contains(x.Source) &&
                    _globalPreferences.Targets
                        .Where(y => y.Item2)
                        .Select(y => y.Item1)
                        .Contains(x.Target))
                .OrderBy(x => x.Target)
                .ToList();

            _dataGridElements = new ObservableCollection<DangerousLevelLine>(_globalPreferences.Dangers);
            DangersGrid.DataContext = _dataGridElements;
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
    }
}
