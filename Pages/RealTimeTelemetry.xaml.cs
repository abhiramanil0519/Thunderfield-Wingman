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
using System.Windows.Threading;

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
            Loaded += RTT_Loaded;
        }

        private async void RTT_Loaded(object sender, RoutedEventArgs e)
        {
            _ = PageNav();

            try
            {
                await Task.Delay(2000);
                await RTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "RTT() crashed");
            }
        }

        private async Task PageNav()
        {
            thunderfield_wingman.Methods.Menu.CustomMenu(
                this,
                new Label[] { CCA },
                new Key[] { Key.D0 },
                new Page[] { new CCA() }
            );
        }

        private async Task RTT()
        {
            string csvPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources", "ground_idle_mission.csv");

            if (!System.IO.File.Exists(csvPath))
            {
                MessageBox.Show($"CSV not found at:\n{csvPath}");
                return;
            }

            var lines = await Task.Run(() => System.IO.File.ReadAllLines(csvPath));
            if (lines.Length < 2)
            {
                MessageBox.Show("CSV found but has no data rows.");
                return;
            }

            var headers = lines[0].Split(',');
            var col = new Dictionary<string, int>();
            for (int i = 0; i < headers.Length; i++)
                col[headers[i].Trim()] = i;

            int Idx(string name) => col.TryGetValue(name, out var i) ? i : -1;

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
                if (lastTime.HasValue)
                {
                    int delayMs = (int)((time - lastTime.Value) * 4000);
                    if (delayMs > 0) await Task.Delay(delayMs);
                }
                lastTime = time;

                void Set(Label lbl, string baseText, double value, string unit, string fmt = "0.##")
                    => lbl.Content = $"{baseText}{value.ToString(fmt, System.Globalization.CultureInfo.InvariantCulture)} {unit}";

                Set(EngineRPM, "ENGINE RPM       : ", GetD("ENGINE_RPM"), "RPM", "0");
                Set(CylHeadTemp, "CYL HEAD TMP  : ", GetD("CYLINDER_HEAD_TEMP"), "°C");
                Set(ExhaustGasTemp, "EGT TMP               : ", GetD("EXHAUST_GAS_TEMP"), "°C");
                Set(OilTemp, "OIL TMP                : ", GetD("OIL_TEMP"), "°C");
                Set(OilPress, "OIL PREss          : ", GetD("OIL_PRESSURE"), "bar");
                Set(FuelFlow, "fuel flow        : ", GetD("FUEL_FLOW"), "L/h");
                Set(FuelPress, "fuel press      : ", GetD("FUEL_PRESSURE"), "bar");
                Set(InjectorTrend, "injEct TREND   : ", GetD("INJECTION_TIMING"), "°");
                Set(AFR, "AFR                        : ", GetD("AFR_GUAGE"), ":1");
                Set(ManifoldPress, "manif Press    : ", GetD("MANIFOLD_PRESSURE"), "kPa");
                Set(CarbAirIntakePress, "CAI  press         : ", GetD("CARB_AIR_INTAKE_PRESSURE"), "kPa");
                Set(BaroPress, "BAR press        : ", GetD("BAROMETRIC PRESSURE"), "kPa");

                Set(CoolantFlow, "COOLANT FLW   : ", GetD("COOLANT_FLOW_RATE"), "L/min");
                Set(CoolantTemp, "COOLANT TMP   : ", GetD("COOLANT_TEMP"), "°C");
                Set(CoolantPress, "COOL PRESS      : ", GetD("COOLANT_PRESSURE"), "bar");
                Set(FuelLevel, "FUEL LEVEL      : ", GetD("FUEL_LEVEL"), "%");
                Set(BattAmp, "BATT AMP           : ", GetD("BATT_AMP"), "A");
                Set(EngineMountVibration, "MOUNT VIBR       : ", GetD("ENGINE_MOUNT_VIBRATION"), "mm/s");
                Set(AlternatorAmp, "ALTR AMP           : ", GetD("ALTERNATOR_AMP"), "A");
                Set(AlternatorVolt, "ALTR VOLT          : ", GetD("ALTERNATOR_VOLT"), "V");
                Set(TurboBoostPress, "TURBO BOOST   : ", GetD("TURBO_TARGET_BOOST_PRESSURE"), "psi");
                Set(Altitude, "ALT                        : ", GetD("FLYING_ALT"), "ft", "0");
                Set(GForce, "G  -   FORCE       : ", GetD("G_FORCE"), "g");
                Set(AirSpeed, "AIR SPD               : ", GetD("AIRSPEED"), "km/h");
            }
        }
    }
}