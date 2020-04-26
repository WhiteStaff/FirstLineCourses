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
using OfficeOpenXml.Packaging.Ionic.Zip;
using ThreatsParser.Entities;
using ThreatsParser.FileActions;
using TreatsParser.Core;

namespace ThreatsParser.Windows
{
    /// <summary>
    /// Логика взаимодействия для PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        private List<ModelLine> _model;

        private GlobalPreferences _globalPreferences;
        
        public PreviewWindow(GlobalPreferences globalPreferences)
        {
            InitializeComponent();
            _globalPreferences = globalPreferences;
            _model = ModelGeneration.GenerateModelForPreview(_globalPreferences)
                .OrderBy(x => x.ThreatName)
                .ThenBy(x => x.Target)
                .ThenBy(x => x.Source)
                .Select((x, y) => new ModelLine(y + 1, x))
                .ToList();
            PreviewModel.ItemsSource = _model;
        }

        private void All_OnSelected(object sender, RoutedEventArgs e)
        {
            PreviewModel.ItemsSource = _model;
        }

        private void Actual_OnSelected(object sender, RoutedEventArgs e)
        {
            PreviewModel.ItemsSource = _model.Where(x => x.isActual == "Актуальная");
        }

        private void NotActual_OnSelected(object sender, RoutedEventArgs e)
        {
            PreviewModel.ItemsSource = _model.Where(x => x.isActual == "Неактуальная");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var win = new PreferencesWindow(_globalPreferences);
            win.Show();
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            FileSaver.Save(_model);
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            _globalPreferences = new GlobalPreferences();
            Close();
        }
    }
}
