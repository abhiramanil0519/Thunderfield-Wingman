using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace thunderfield_wingman.Methods
{
    public class Menu
    {
        public static async Task<string> InitMenu(
            string menuTitle,
            string[] menuItems,
            Grid targetGrid)
        {
            targetGrid.Children.Clear();

            // Menu title
            Label title = new Label
            {
                Content = menuTitle,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10, 10, 0, 0),
                Foreground = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#66ff66")),
                FontFamily = new FontFamily("MS33558 Slim"),
                FontSize = 15
            };

            targetGrid.Children.Add(title);

            // Menu items
            foreach (string xaml in menuItems)
            {
                string fullXaml = xaml.Replace(
                    "<Label ",
                    "<Label xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" ");

                UIElement element = (UIElement)XamlReader.Parse(fullXaml);
                targetGrid.Children.Add(element);
            }

            // Wait for keyboard input
            string userInput = await WaitForInput(targetGrid);

            int index;

            // Number input
            if (int.TryParse(userInput, out index))
            {
                if (index >= 0 && index < menuItems.Length)
                {
                    if (targetGrid.Children[index + 1] is Control selected)
                    {
                        selected.Foreground = new SolidColorBrush(
                            (Color)ColorConverter.ConvertFromString("#ff10f0"));
                    }
                }
            }

            // "*" selects last menu item
            else if (userInput == "*")
            {
                if (targetGrid.Children.Count > 1)
                {
                    if (targetGrid.Children[targetGrid.Children.Count - 1] is Control selected)
                    {
                        selected.Foreground = new SolidColorBrush(
                            (Color)ColorConverter.ConvertFromString("#ff00ff"));
                    }
                }
            }

            await Task.Delay(500);

            return userInput;
        }

        private static Task<string> WaitForInput(Grid targetGrid)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            void KeyDownHandler(object sender, KeyEventArgs e)
            {
                if (e.Key >= Key.D0 && e.Key <= Key.D9)
                {
                    string input = (e.Key - Key.D0).ToString();

                    targetGrid.KeyDown -= KeyDownHandler;
                    tcs.SetResult(input);
                }
                else if (e.Key == Key.Oem8 || e.Key == Key.Multiply)
                {
                    targetGrid.KeyDown -= KeyDownHandler;
                    tcs.SetResult("*");
                }
            }

            targetGrid.Focusable = true;
            targetGrid.Focus();
            targetGrid.KeyDown += KeyDownHandler;

            return tcs.Task;
        }
    }
}
