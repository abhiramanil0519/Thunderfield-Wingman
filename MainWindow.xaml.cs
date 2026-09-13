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


        /* ENGINE SELECTION MENU */
        string result = await thunderfield_wingman.Methods.Menu.InitMenu(
            "Menu title:",
            new string[]
            {
            "<Label Content=\"1  ROTAX 914F TURBO - 1200CC\" HorizontalAlignment=\"Left\" Margin=\"10,36,0,0\" Foreground=\"#66ff66\" FontFamily=\"/Thunderfield-Wingman;component/Fonts/#MS33558 Slim\" FontSize=\"15\"/>",
            "<Label Content=\"2  HONEYWELL TPE331-10/12/14 TURBOPROP - ?? \" HorizontalAlignment=\"Left\" Margin=\"10,55,0,0\" Foreground=\"#66ff66\" FontFamily=\"/Thunderfield-Wingman;component/Fonts/#MS33558 Slim\" FontSize=\"15\"/>",
            "<Label Content=\"*  CREATE NEW ENGINE\" HorizontalAlignment=\"Left\" Margin=\"10,74,0,0\" Foreground=\"#66ff66\" FontFamily=\"/Thunderfield-Wingman;component/Fonts/#MS33558 Slim\" FontSize=\"15\"/>"

            },
            menuPage.DisplayGrid
        );

        MainFrame.Navigate(new Aligning());



    }
}
