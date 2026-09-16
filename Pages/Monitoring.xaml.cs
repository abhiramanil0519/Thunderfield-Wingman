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
using System.Windows.Threading;

namespace thunderfield_wingman.Pages
{
    /// <summary>
    /// Interaction logic for Monitoring.xaml
    /// </summary>
    public partial class Monitoring : Page
    {
        public Monitoring()
        {
            InitializeComponent();
            Loaded += MON_Loaded;
        }

        private void MON_Loaded(object sender, RoutedEventArgs e)
        {
            _ = BlinkMON();
            _ = Err();
        }

        private async Task BlinkMON()
        {
            while (true)
            {
                MON.Opacity = 0;
                await Task.Delay(1000);
                MON.Opacity = 1;
                await Task.Delay(1000);
            }
        }

        private async Task Err()
        {
            // fault_mission.csv is expected next to the .exe (same pattern as ground_idle_mission.csv);
            // falls back to a Resources subfolder if you set Copy to Output Directory to preserve the folder.
            string csvPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fault_mission.csv");
            if (!System.IO.File.Exists(csvPath))
                csvPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "fault_mission.csv");

            if (!System.IO.File.Exists(csvPath))
                return;

            var lines = await Task.Run(() => System.IO.File.ReadAllLines(csvPath));
            if (lines.Length < 2) return;

            var headers = lines[0].Split(',');
            var col = new Dictionary<string, int>();
            for (int i = 0; i < headers.Length; i++)
                col[headers[i].Trim()] = i;

            int Idx(string name) => col.TryGetValue(name, out var i) ? i : -1;

            var rng = new Random();
            double rul = 100.0;        // remaining useful life, starts at 100%
            bool engineFailure = false;
            double? lastTime = null;

            for (int row = 1; row < lines.Length; row++)
            {
                var f = lines[row].Split(',');

                double GetD(string name)
                {
                    int i = Idx(name);
                    if (i < 0 || i >= f.Length) return 0;
                    return double.TryParse(f[i], System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var v) ? v : 0;
                }

                double time = GetD("TIME_S");
                double altitude = GetD("FLYING_ALT");

                if (lastTime.HasValue)
                {
                    int delayMs = (int)((time - lastTime.Value) * 500);
                    if (delayMs > 0) await Task.Delay(delayMs);
                }
                lastTime = time;

                
                if (!engineFailure && time > 100 && altitude >= 7100)
                {
                    engineFailure = true;

                    ALRT.Opacity = 1;
                    await Task.Delay(500);
                    FAIL_INJ_1.Opacity = 1;
                    await Task.Delay(200);
                    FAIL_INJ_2.Opacity = 1;

                    await Task.Delay(3000);
                    
                    TURBO.Opacity = 1;
                    await Task.Delay(200);
                    FAIL_TURBO.Opacity = 1;

                    await Task.Delay(400);

                    for (int i = 0; i < 5; i++)
                    {
                        await Task.Delay(300);
                        R1.Opacity = 0;
                        await Task.Delay(300);
                        R2.Opacity = 1;

                        await Task.Delay(300);
                        R1.Opacity = 0;
                        await Task.Delay(300);
                        R2.Opacity = 1;

                        await Task.Delay(1000);
                    }

                }

                
                if (!engineFailure)
                {
                    // Nominal operation: slow, near-flat wear with tiny random drift
                    double wear = 0.01 + rng.NextDouble() * 0.01;
                    rul -= wear;
                }
                else
                {
                    // Engine failure detected: RUL collapses toward zero exponentially,
                    // with a bit of jitter so it doesn't look like a perfect curve
                    double decayRate = 0.06;                       // ~6% of remaining life lost per tick
                    double noise = rng.NextDouble() * 0.4;         // small extra jitter, always eating into RUL
                    rul -= (rul * decayRate) + noise;
                }

                rul = Math.Max(0, Math.Min(100, rul));

                RUL.Content = $"RUL             {rul.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}   ";
            }
        }
    }
}