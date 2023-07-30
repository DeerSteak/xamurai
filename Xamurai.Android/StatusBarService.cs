using Android.App;
using Xamarin.Forms;
using Xamurai.Droid;
using Xamurai.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarService))]
namespace Xamurai.Droid
{
    public class StatusBarService : IStatusBarService
    {
        public void Hide()
        {
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            var controller = activity.Window.DecorView.WindowInsetsController;
            controller.Hide(Android.Views.WindowInsets.Type.StatusBars());
        }

        public void Show()
        {
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            var controller = activity.Window.DecorView.WindowInsetsController;
            controller.Show(Android.Views.WindowInsets.Type.StatusBars());
        }

        public double GetHeight()
        {
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            var resourceId = activity.Resources.GetIdentifier("navigation_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                return activity.Resources.GetDimensionPixelSize(resourceId) / activity.Resources.DisplayMetrics.Density;
            }

            return 50;
        }
    }
}

