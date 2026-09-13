using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using thunderfield_wingman.Pages;

namespace thunderfield_wingman.Methods
{
    public class WingmanUI
    {
        // Boot Animation
        public static async void StartBootAnim(Frame mainFrame)
        {
            await Task.Delay(1000);

            mainFrame.Opacity = 0;

            mainFrame.Navigate(new Boot());

            var fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.7)
            };

            mainFrame.BeginAnimation(
                System.Windows.UIElement.OpacityProperty,
                fadeIn);
        }
    }

}