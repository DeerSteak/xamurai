using System;
using Xamarin.Forms;

namespace Xamurai
{
    public class CarDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MercedesDataTemplate { get; set; }
        public DataTemplate OtherDataTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Car car)
            {
                if (car.Make == CarMake.Mercedes)
                {
                    return MercedesDataTemplate;
                }
                return OtherDataTemplate;
            }
            return null;
        }
    }
}

