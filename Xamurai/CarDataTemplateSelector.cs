using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xamurai
{
    public class CarDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MercedesDataTemplate { get; set; }
        public DataTemplate OtherDataTemplate { get; set; }
        public DataTemplate iOSMercedesDataTemplate { get; set; }
        public DataTemplate iOSOtherDataTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Car car)
            {
                if (car.Make == CarMake.Mercedes)
                {
                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                        return iOSMercedesDataTemplate;
                    return MercedesDataTemplate;
                }
                if (DeviceInfo.Platform == DevicePlatform.iOS)
                    return iOSOtherDataTemplate;
                return OtherDataTemplate;
            }
            return null;
        }
    }
}

