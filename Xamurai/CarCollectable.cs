using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace Xamurai
{
    public class CarCollectable : BindableBase
    {
        private ObservableCollection<Car> _collectedCars;

        public ObservableCollection<Car> CollectedCars
        {
            get => _collectedCars;
            set => SetProperty(ref _collectedCars, value);
        }
    }
}

