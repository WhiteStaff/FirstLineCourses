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
using ThreatsParser.Entities.Enums;

namespace ThreatsParser.Windows
{
    /// <summary>
    /// Логика взаимодействия для PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        private GlobalPreferences _globalPreferences;

        public PreferencesWindow(GlobalPreferences globalPreferences)
        {
            InitializeComponent();
            _globalPreferences = globalPreferences;

            if (_globalPreferences.InitialSecurityLevel == null)
                _globalPreferences.InitialSecurityLevel = new InitialSecurityLevel();

            stackPanelTerritorial.Children.OfType<RadioButton>().FirstOrDefault(x =>
                int.Parse(x.Name.Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Last()) ==
                (int) _globalPreferences.InitialSecurityLevel.TerritorialLocation).IsChecked = true;

            stackPanelNetwork.Children.OfType<RadioButton>().FirstOrDefault(x =>
                int.Parse(x.Name.Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Last()) ==
                (int) _globalPreferences.InitialSecurityLevel.NetworkCharacteristic).IsChecked = true;

            stackPanelPDAction.Children.OfType<RadioButton>().FirstOrDefault(x =>
                int.Parse(x.Name.Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Last()) ==
                (int) _globalPreferences.InitialSecurityLevel.PersonalDataActionCharacteristics).IsChecked = true;

            stackPanelSplit.Children.OfType<RadioButton>().FirstOrDefault(x =>
                int.Parse(x.Name.Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Last()) ==
                (int) _globalPreferences.InitialSecurityLevel.PersonalDataActionCharacteristics).IsChecked = true;

            stackPanelDBConn.Children.OfType<RadioButton>().FirstOrDefault(x =>
                int.Parse(x.Name.Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Last()) ==
                (int) _globalPreferences.InitialSecurityLevel.OtherDbConnections).IsChecked = true;

            stackPanelAnonLevel.Children.OfType<RadioButton>().FirstOrDefault(x =>
                int.Parse(x.Name.Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Last()) ==
                (int) _globalPreferences.InitialSecurityLevel.AnonymityLevel).IsChecked = true;

            stackPanelPDSharing.Children.OfType<RadioButton>().FirstOrDefault(x =>
                int.Parse(x.Name.Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries).Last()) ==
                (int) _globalPreferences.InitialSecurityLevel.PersonalDataSharingLevel).IsChecked = true;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var second = new SecondGenerationStepWindow(_globalPreferences);
            second.Show();
            Close();
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var x = sender as RadioButton;
            if (_globalPreferences == null) return;
            switch (x.GroupName)
            {
                case "Territorial":
                    _globalPreferences.InitialSecurityLevel.TerritorialLocation =
                        (LocationCharacteristic) int.Parse(x.Name.Split(new[] {'s'}, StringSplitOptions.RemoveEmptyEntries)
                            .Last());
                    break;
                case "Network":
                    _globalPreferences.InitialSecurityLevel.NetworkCharacteristic =
                        (NetworkCharacteristic)int.Parse(x.Name.Split(new[] { 's' }, StringSplitOptions.RemoveEmptyEntries)
                            .Last());
                    break;
                case "PDAction":
                    _globalPreferences.InitialSecurityLevel.PersonalDataActionCharacteristics =
                        (PersonalDataActionCharacteristics)int.Parse(x.Name.Split(new[] { 's' }, StringSplitOptions.RemoveEmptyEntries)
                            .Last());
                    break;
                case "Split":
                    _globalPreferences.InitialSecurityLevel.PersonalDataPermissionSplit =
                        (PersonalDataPermissionSplit)int.Parse(x.Name.Split(new[] { 's' }, StringSplitOptions.RemoveEmptyEntries)
                            .Last());
                    break;
                case "DBConn":
                    _globalPreferences.InitialSecurityLevel.OtherDbConnections =
                        (OtherDBConnections)int.Parse(x.Name.Split(new[] { 's' }, StringSplitOptions.RemoveEmptyEntries)
                            .Last());
                    break;
                case "AnonLevel":
                    _globalPreferences.InitialSecurityLevel.AnonymityLevel =
                        (AnonymityLevel)int.Parse(x.Name.Split(new[] { 's' }, StringSplitOptions.RemoveEmptyEntries)
                            .Last());
                    break;
                default:
                    _globalPreferences.InitialSecurityLevel.PersonalDataSharingLevel =
                        (PersonalDataSharingLevel)int.Parse(x.Name.Split(new[] { 's' }, StringSplitOptions.RemoveEmptyEntries)
                            .Last());
                    break;
            }
        }
    }
}