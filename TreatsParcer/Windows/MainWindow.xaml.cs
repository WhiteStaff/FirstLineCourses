using System.Windows;
using ThreatsParser.Entities;
using ThreatsParser.FileActions;
using ThreatsParser.Windows;

namespace ThreatsParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GlobalPreferences _globalPreferences;

        public MainWindow()
        {
            InitializeComponent();
            _globalPreferences = FileCreator.GetParsedData();
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new FirstGenerationStep(_globalPreferences).Show();
        }
    }
}