
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamurai
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagedCollectionPage : ContentPage
    {
        public int ColumnWidth;
        public PagedCollectionPage()
        {
            var vm = new SampleViewModel();
            BindingContext = vm;
            vm.SetCarCollectables();
            DeviceDisplay.MainDisplayInfoChanged += (s, e) =>
            {
                vm.SetCarCollectables();
            };
            InitializeComponent();
        }
    }
}