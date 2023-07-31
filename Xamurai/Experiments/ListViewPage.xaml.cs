using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Xamurai.Interfaces;

namespace Xamurai
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        IStatusBarService _statusBarService;

        public ListViewPage()
        {
            BindingContext = new SampleViewModel(true);
            Xamarin.Forms.Application.Current.On<iOS>().SetPanGestureRecognizerShouldRecognizeSimultaneously(true);            
            InitializeComponent();
            if (DeviceDisplay.MainDisplayInfo.Height < DeviceDisplay.MainDisplayInfo.Width)
                _cv.ItemsLayout = LinearItemsLayout.Horizontal;
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
            _statusBarService = DependencyService.Get<IStatusBarService>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _statusBarService?.Hide();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _statusBarService.Show();
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            var isPortrait = e.DisplayInfo.Height > e.DisplayInfo.Width;
            _cv.ItemsLayout = isPortrait ? LinearItemsLayout.Vertical : LinearItemsLayout.Horizontal;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.BindingContext is Car car)
                {
                    (BindingContext as SampleViewModel).RemoveCar(car);
                }
            }
        }

        void PanGestureRecognizer_PanUpdated(System.Object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            if (sender is Grid grid)
            {
                var button = grid.Children.Where(c => c.GetType() == typeof(Button)).FirstOrDefault();
                if (button != null)
                {
                    Console.WriteLine($"x: {e.TotalX}, y: {e.TotalY}");

                    if (e.StatusType == GestureStatus.Running && e.TotalX < 0 && (uint)e.TotalX > (uint)e.TotalY)
                    {
                        button.IsVisible = true;
                    }
                    else if (e.StatusType == GestureStatus.Running && e.TotalX > 0 && (uint)e.TotalX > (uint)e.TotalY)
                    {
                        button.IsVisible = false;
                    }
                }
            }
        }
    }
}