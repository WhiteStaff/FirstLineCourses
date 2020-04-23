using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThreatsParser.Entities;
using TreatsParser.Core.DataBase;

namespace ThreatsParser.Windows
{
    /// <summary>
    /// Логика взаимодействия для FirstGenerationStep.xaml
    /// </summary>
    public partial class FirstGenerationStep : Window
    {
        private GlobalPreferences _globalPreferences;

        public FirstGenerationStep(GlobalPreferences globalPreferences)
        {
            InitializeComponent();
            _globalPreferences = globalPreferences;

            using (var context = new ThreatDbContext())
            {
                if (_globalPreferences.Source == null)
                    _globalPreferences.Source = context.Source.Select(x => x.Source).Distinct().OrderBy(x => x).ToList()
                        .Select(x => (x, true)).ToList();
                if (_globalPreferences.Targets == null)
                    _globalPreferences.Targets = context.Target.Select(x => x.Type).Distinct().OrderBy(x => x).ToList()
                        .Select(x => (x, true)).ToList();
            }

            _globalPreferences.Source.Select((val, i) =>
                new CheckBox
                {
                    Name = $"_{i}",
                    IsChecked = val.Item2,
                    Content = new TextBlock {Text = val.Item1, TextWrapping = TextWrapping.Wrap}
                }).ToList().ForEach(x => Source.Children.Add(x));

            _globalPreferences.Targets.Select((val, i) =>
                new CheckBox
                {
                    Name = $"_{i}",
                    IsChecked = val.Item2,
                    Content = new TextBlock {Text = val.Item1, TextWrapping = TextWrapping.Wrap}
                }).ToList().ForEach(x => Target.Items.Add(x));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _globalPreferences.Source = (from CheckBox curr in Source.Children
                select (((TextBlock) curr.Content).Text, (bool) curr.IsChecked)).ToList();
           _globalPreferences.Targets = (from CheckBox curr in Target.Items
                select (((TextBlock) curr.Content).Text, (bool) curr.IsChecked)).ToList();

            var secondWindow = new SecondGenerationStepWindow(_globalPreferences);
            secondWindow.Show();
            Close();
        }
    }
}