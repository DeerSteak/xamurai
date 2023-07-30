using Xamarin.Essentials;
using Xamarin.Forms;
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

        void SwipeItem_Invoked(System.Object sender, System.EventArgs e)
        {
            if (sender is SwipeItem swipeItem)
            {
                if (swipeItem.BindingContext is Car car)
                {
                    (BindingContext as SampleViewModel).RemoveCar(car);
                }
            }
        }
    }
}