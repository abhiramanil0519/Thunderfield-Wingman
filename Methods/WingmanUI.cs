using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using thunderfield_wingman.Pages;

namespace thunderfield_wingman.Methods
{
    public class WingmanUI
    {
        // Boot Animation
        public static async Task StartBootAnim(Frame mainFrame)
        {
            mainFrame.Opacity = 0;
            mainFrame.Navigate(new Boot());

            var fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };

            mainFrame.BeginAnimation(UIElement.OpacityProperty, fadeIn);

            await Task.Delay(1000);
        }
    }

}