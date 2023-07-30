using UIKit;
using Xamarin.Essentials;
using Xamurai.Interfaces;
using Xamurai.iOS;


[assembly: Xamarin.Forms.Dependency(typeof(StatusBarService))]
namespace Xamurai.iOS
{
    public class StatusBarService : IStatusBarService
    {
        public void Hide()
        {
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        public void Show()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }

        public double GetHeight()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var topPadding = window.SafeAreaInsets.Top;
                var bottomPadding = window.SafeAreaInsets.Bottom;

                return topPadding + bottomPadding;
            }
            else
            {
                return 100;
            }
        }
    }
}

