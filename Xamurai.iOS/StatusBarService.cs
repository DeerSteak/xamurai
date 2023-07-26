using UIKit;
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
    }
}

