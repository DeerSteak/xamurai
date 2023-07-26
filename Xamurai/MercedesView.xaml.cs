using System.ComponentModel;
using System.Windows.Input;
using Prism.Commands;
using Xamarin.Forms;

namespace Xamurai
{
    public partial class MercedesView : ContentView, INotifyPropertyChanged
    {
        public MercedesView()
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
            car.WasToggled?.Invoke();

            OnPropertyChanged(nameof(IsExpanded));
        }

        public ICommand ToggleCollapseCommand { get; }

        public bool IsExpanded { get; set; }
    }
}

