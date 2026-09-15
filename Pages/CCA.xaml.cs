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
    /// Interaction logic for CCA.xaml
    /// </summary>
    public partial class CCA : Page
    {
        public CCA()
        {
            InitializeComponent();
            Loaded += CCA_Loaded;
        }

        private async void CCA_Loaded(object sender, RoutedEventArgs e)
        {
            CommsStat.Opacity = 0;
            IBITStat.Opacity = 0;
            MBGTStat.Opacity = 0;
            RangeStat.Opacity = 0;

            await Task.Delay(500);

            NOSOI.Opacity = 0;
            CommsStat.Opacity = 1;

            await Task.Delay(2000);

            RangeStat.Opacity = 1;
            RangeRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));

            await Task.Delay(3000);

            MBGTStat.Opacity = 1;
            MBGTRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));

            await Task.Delay(6000);

            IBITStat.Opacity = 1;
            IBITRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));

            await Task.Delay(500);

            for (int i = 0; i < 4; i++)
            {
                IBITStat.Opacity = 0;
                await Task.Delay(500);
                IBITStat.Opacity = 1;
                await Task.Delay(500);
            }

            await Task.Delay(500);
            IBITStat.Opacity = 1;

            NavigationService.Navigate(new Aligning());
            await Task.Delay(500);
        }
    }
}
