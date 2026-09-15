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
using thunderfield_wingman.Methods;

namespace thunderfield_wingman.Pages
{
    /// <summary>
    /// Interaction logic for RealTimeTEelemetry.xaml
    /// </summary>
    public partial class RealTimeTEelemetry : Page
    {
        public RealTimeTEelemetry()
        {
            InitializeComponent();

            thunderfield_wingman.Methods.Menu.CustomMenu(
                this,
                new Label[] { CCA },
                new Key[] { Key.D0 },
                new Page[] { new CCA() }
            );
        }
    }
}