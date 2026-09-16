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

            await Task.Delay(2000);

            NOSOI.Opacity = 0;

            await Task.Delay(700);

            CommsStat.Opacity = 1;

            await Task.Delay(2000);

            RangeStat.Opacity = 1;
            RangeRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));

            await Task.Delay(3800);

            MBGTStat.Opacity = 1;
            MBGTRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));

            await Task.Delay(6500);

            IBITStat.Opacity = 1;
            IBITRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));

            await Task.Delay(500);

            for (int i = 0; i < 4; i++)
            {
                // IBITStat.Opacity = 0;
                IBITRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                IBITRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                IBITStat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                IBIT.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

                await Task.Delay(500);

                // IBITStat.Opacity = 1;
                IBITRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));
                IBITRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                IBITStat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));
                IBIT.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66FF66"));

                await Task.Delay(500);
            }

            for (int i = 0; i < 2; i++)
            {
                IBITStat.Opacity = 0;
                await Task.Delay(500);

                IBITStat.Opacity = 1;
                await Task.Delay(500);
            }

            await Task.Delay(1500);
            IBITStat.Opacity = 1;

            await Task.Delay(2500);

            NavigationService.Navigate(new Monitoring());
        }
    }
}
