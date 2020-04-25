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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new FirstGenerationStep(_globalPreferences).Show();
        }
    }
}