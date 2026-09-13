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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace thunderfield_wingman.Pages
{
    /// <summary>
    /// Interaction logic for Aligning.xaml
    /// </summary>
    public partial class Aligning : Page
    {
        public Aligning()
        {
            InitializeComponent();
            Loaded += Aligning_Loaded;
        }

        private async void Aligning_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                LoadingText.Content = "ALIGNING";
                await Task.Delay(300);
                LoadingText.Content = "ALIGNING.";
                await Task.Delay(300);
                LoadingText.Content = "ALIGNING..";
                await Task.Delay(300);
                LoadingText.Content = "ALIGNING...";
                await Task.Delay(300);
            }
        }
    }
}
