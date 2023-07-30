using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamurai
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MercedesView : ContentView, INotifyPropertyChanged
    {
        public MercedesView()
        {
            InitializeComponent();
        }

        void Expander_Tapped(System.Object sender, System.EventArgs e)
        {
            if (sender is Xamarin.CommunityToolkit.UI.Views.Expander expander)
            {
                if (expander.BindingContext is Car car)
                {
                    car.CollapseCommand?.Execute(null);
                }
            }
        }
    }
}

