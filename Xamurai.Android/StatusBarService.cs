using Xamurai.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarService))]
namespace Xamurai.Droid
{
    public class StatusBarService : IStatusBarService
    {
        public void Hide()
        {
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            var attributes = activity.Window.Attributes;
            attributes.Flags |= Android.Views.WindowManagerFlags.Fullscreen;
        }

        public void Show()
        {
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            activity.Window.ClearFlags(Android.Views.WindowManagerFlags.Fullscreen);
        }
    }
}

