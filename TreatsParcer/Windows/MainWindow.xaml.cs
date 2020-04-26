using System.Windows;
using ThreatsParser.Entities;
using ThreatsParser.FileActions;
using ThreatsParser.MenuActions;
using ThreatsParser.MenuActions.Interfaces;
using ThreatsParser.Windows;

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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new FirstGenerationStep(_globalPreferences).Show();
        }
    }
}