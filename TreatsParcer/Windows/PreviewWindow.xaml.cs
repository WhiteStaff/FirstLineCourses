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
using TreatsParser.Core;

namespace ThreatsParser.Windows
{
    /// <summary>
    /// Логика взаимодействия для PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        public PreviewWindow(GlobalPreferences _globalPreferences)
        {
            InitializeComponent();
            PreviewModel.ItemsSource = ModelGeneration.GenerateModelForPreview(_globalPreferences.Items,
                _globalPreferences.InitialSecurityLevel.GlobalCoef);
        }
    }
}
