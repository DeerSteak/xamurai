using Prism.Commands;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Xamurai
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarView : ContentView, INotifyPropertyChanged
    {
        public CarView()
        {
            IsExpanded = true;
            BindingContextChanged += CarView_BindingContextChanged;
            ToggleCollapseCommand = new DelegateCommand(ToggleCollapse);
            InitializeComponent();
        }

        private void CarView_BindingContextChanged(object sender, System.EventArgs e)
        {
            IsExpanded = (BindingContext as Car)?.IsVisible ?? true;
            OnPropertyChanged(nameof(IsExpanded));
        }

        private void ToggleCollapse()
        {
            IsExpanded = !IsExpanded;
            var car = BindingContext as Car;
            car.IsVisible = IsExpanded;
            car.IsToggled();

            OnPropertyChanged(nameof(IsExpanded));
        }

        public ICommand ToggleCollapseCommand { get; }

        public bool IsExpanded { get; set; }
    }
}