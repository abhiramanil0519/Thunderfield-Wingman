using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using thunderfield_wingman.Methods;
using thunderfield_wingman.Pages;

namespace thunderfield_wingman;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await Task.Delay(500);
        await WingmanUI.StartBootAnim(MainFrame);
        await Task.Delay(1000);

        MainFrame.Navigate(new Aligning());
        await Task.Delay(7200);

        DisplayMenu menuPage = new DisplayMenu();
        MainFrame.Navigate(menuPage);

        string MenuResult;

        while (true)
        {
            menuPage = new DisplayMenu();
            MainFrame.Navigate(menuPage);

            MenuResult = await thunderfield_wingman.Methods.Menu.InitMenu(
                "2 ENGINES FOUND:",
                new string[]
                {
                    "<Label Content=\"0   ROTAX 914F TURBO - 1200CC\" HorizontalAlignment=\"Left\" Margin=\"10,36,0,0\" Foreground=\"#66ff66\" FontFamily=\"/Thunderfield-Wingman;component/Fonts/#MS33558 Slim\" FontSize=\"15\"/>",
                    "<Label Content=\"1   HONEYWELL TPE331-10/12/14 TURBOPROP - ??\" HorizontalAlignment=\"Left\" Margin=\"10,55,0,0\" Foreground=\"#66ff66\" FontFamily=\"/Thunderfield-Wingman;component/Fonts/#MS33558 Slim\" FontSize=\"15\"/>",
                    "<Label Content=\"*   CREATE NEW ENGINE\" HorizontalAlignment=\"Left\" Margin=\"10,74,0,0\" Foreground=\"#66ff66\" FontFamily=\"/Thunderfield-Wingman;component/Fonts/#MS33558 Slim\" FontSize=\"15\"/>"
                },
                menuPage.DisplayGrid
            );

            if (MenuResult == "0")
            {
                MainFrame.Navigate(new Aligning());
                await Task.Delay(1500);
                break;
            }

            if (MenuResult == "1")
            {
                MessageBox.Show("HONEYWELL TPE331-10/12/14 not yet configured!");
                continue;
            }

            if (MenuResult == "*")
            {
                MessageBox.Show("This feature not yet available!");
                continue;
            }
        }

        /* TELEMETRY SELECTION MENU */
        while (true)
        {
            menuPage = new DisplayMenu();
            MainFrame.Navigate(menuPage);

            MenuResult = await thunderfield_wingman.Methods.Menu.InitMenu(
                "SELECT TELEMETRY FEED TYPE:",
                new string[]
                {
                    "<Label Content=\"0   REAL-TIME TELEMETRY ( 360 Hz )\" HorizontalAlignment=\"Left\" Margin=\"10,36,0,0\" Foreground=\"#66ff66\" FontFamily=\"/Thunderfield-Wingman;component/Fonts/#MS33558 Slim\" FontSize=\"15\"/>",
                    "<Label Content=\"*   UPLOAD MISSION DAT -- GCS      [ _ _  / _ _ _  ]\" HorizontalAlignment=\"Left\" Margin=\"10,55,0,0\" Foreground=\"#66ff66\" FontFamily=\"/Thunderfield-Wingman;component/Fonts/#MS33558 Slim\" FontSize=\"15\"/>"
                },
                menuPage.DisplayGrid
            );

            if (MenuResult == "0")
            {
                MainFrame.Navigate(new RealTimeTEelemetry());

                break;
            }

            if (MenuResult == "*")
            {
                // mission.dat
                continue;
            }


        }

    }
}