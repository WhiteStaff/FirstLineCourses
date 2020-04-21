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
using ThreatsParser.Entities;
using ThreatsParser.Entities.Enums;

namespace ThreatsParser.Windows
{
    /// <summary>
    /// Логика взаимодействия для PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        public InitialSecurityLevel InitialSecurityLevel { get; set; }

        public PreferencesWindow(ref InitialSecurityLevel initialSecurityLevel)
        {
            InitializeComponent();
            InitialSecurityLevel = initialSecurityLevel;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var a = new InitialSecurityLevel();
            var b = a;
            b.AnonymityLevel = AnonymityLevel.OnlyInside;

            InitialSecurityLevel.TerritorialLocation = (LocationCharacteristic) stackPanelTerritorial.Children
                .OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true).Name
                .Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Last();

            InitialSecurityLevel.NetworkCharacteristic = (NetworkCharacteristic) stackPanelNetwork.Children
                .OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true).Name
                .Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Last();

            InitialSecurityLevel.PersonalDataActionCharacteristics =
                (PersonalDataActionCharacteristics) stackPanelPDAction.Children
                    .OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true).Name
                    .Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Last();

            InitialSecurityLevel.PersonalDataPermissionSplit = (PersonalDataPermissionSplit) stackPanelSplit
                .Children
                .OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true).Name
                .Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Last();

            InitialSecurityLevel.OtherDbConnections = (OtherDBConnections) stackPanelDBConn.Children
                .OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true).Name
                .Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Last();

            InitialSecurityLevel.AnonymityLevel = (AnonymityLevel) stackPanelAnonLevel.Children
                .OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true).Name
                .Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Last();

            InitialSecurityLevel.PersonalDataSharingLevel = (PersonalDataSharingLevel) stackPanelPDSharing.Children
                .OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true).Name
                .Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Last();

            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}