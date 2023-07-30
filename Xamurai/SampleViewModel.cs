using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamurai.Interfaces;

namespace Xamurai
{
    public class SampleViewModel : BindableBase
    {
        private IStatusBarService _statusBarService;

        public SampleViewModel(bool allowDelete = false)
        {
            _statusBarService = DependencyService.Get<IStatusBarService>();
            GridSpan = Device.Idiom == TargetIdiom.Phone ? 1 : 2;
            BuildCars(allowDelete);
            AskRemoveCarCommand = new Command<Car>(AskRemoveCar);
            LongTouchCommand = new Command(() => Application.Current.MainPage.DisplayAlert("OK!", "OK!", "OK!"));
        }

        //added for PagedCollectionPage challenge #1
        private int _columnWidth;

        public int ColumnWidth
        {
            get => _columnWidth;
            set => SetProperty(ref _columnWidth, value);
        }

        private int _screenWidth;

        public int ScreenWidth
        {
            get => _screenWidth;
            set => SetProperty(ref _screenWidth, value);
        }

        private int _columnHeight;

        public int ColumnHeight
        {
            get => _columnHeight;
            set => SetProperty(ref _columnHeight, value);
        }

        private void SetColumnWidth()
        {
            //get screen width in points.
            double screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            ScreenWidth = (int)screenWidth;

            //column is half of screen width but remove 10px on either side
            //for margin; two columns so three total margins
            ColumnWidth = (int)(ScreenWidth) / 2;

        }

        private void SetColumnHeight()
        {
            //get screen height in points
            double screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;

            //quick and dirty account for status bar and navigation bar
            var navBarHeight = _statusBarService.GetHeight();
            ColumnHeight = (int)(screenHeight - navBarHeight);
        }

        private ObservableCollection<CarCollectable> _carCollectables;

        public ObservableCollection<CarCollectable> CarCollectables
        {
            get => _carCollectables;
            set => SetProperty(ref _carCollectables, value);
        }

        public void SetCarToggle(Car car)
        {
            car.IsVisible = !car.IsVisible;
            SetCarCollectables();
        }

        public void SetCarCollectables()
        {
            Console.WriteLine("Setting collectable collection");
            if (_isToggling)
                return;

            _isToggling = true;
            var column = 1;

            var collectables = new List<CarCollectable>();
            var collectedCars = new List<Car>();
            var collectable = GetNewCollectable();
            var currentHeight = 0;
            var sizeConv = new FontSizeConverter();

            //font size + spacing; I know this works on iPhone 14 and Pixel 5 for sure. 
            var textSize = (double)sizeConv.ConvertFromInvariantString("Default") * 2;

            foreach (var car in Cars)
            {
                var split = car.Description.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                var descLines = split.Length;

                //padding of 5 on top/bottom, padding of 1 on top/bottom, margin of 7 on top == 19
                var cardHeight = (int)textSize + 19;
                if (car.IsVisible)
                    cardHeight += (int)(textSize * descLines);
                currentHeight += cardHeight;
                if (currentHeight > ColumnHeight)
                {
                    if (column == 1)
                    {
                        column = 2;
                    }
                    else
                    {
                        column = 1;
                        collectable.CollectedCars = new ObservableCollection<Car>(collectedCars);
                        collectables.Add(collectable);

                        collectedCars = new List<Car>();
                        collectable = GetNewCollectable();
                    }
                    currentHeight = cardHeight;

                }
                collectedCars.Add(car);
            }
            collectable.CollectedCars = new ObservableCollection<Car>(collectedCars);
            collectables.Add(collectable);

            CarCollectables = new ObservableCollection<CarCollectable>(collectables);
            _isToggling = false;
        }

        private CarCollectable GetNewCollectable()
        {
            return new CarCollectable()
            {
                CollectedCars = new ObservableCollection<Car>()
            };
        }
        //end added for PagedCollectionPage challenge #1

        //added for ListViewPage challenge #2
        public void RemoveCar(Car carToRemove)
        {
            Cars.Remove(carToRemove);
        }

        public async void AskRemoveCar(Car carToRemove)
        {
            var result = await Application.Current.MainPage.DisplayAlert("Delete?", $"Are you sure you want to delete {carToRemove.Name}?", "Delete", "Cancel");
            if (result)
                RemoveCar(carToRemove);
        }

        public ICommand AskRemoveCarCommand;
        public ICommand LongTouchCommand;
        //end added for ListViewPage challenge #2

        private int _gridSpan;

        public int GridSpan
        {
            get { return _gridSpan; }
            set { SetProperty(ref _gridSpan, value); }
        }

        private void BuildCars(bool allowDelete = false)
        {
            SetColumnHeight();
            SetColumnWidth();

            var cars = new ObservableCollection<Car>
            {
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Notes = "test car", Description = "Some description", Color = Color.Black },
                new Car { Abbreviation = "BMW", Make=CarMake.BMW, Name = "X5", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 10)), Color = Color.Purple },
                new Car { Abbreviation = "M", Make=CarMake.Mercedes, Name = "AMG C Class", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 5)), Color = Color.CornflowerBlue},
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = "Some description", Color = Color.Brown },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 3)), Color = Color.Orange },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = "Some description", Color = Color.DarkBlue },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 7)), Color = Color.DarkOrange },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = "Some description", Color = Color.OrangeRed },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 4)), Color = Color.Violet},
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 2)), Color = Color.DarkSalmon },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = "Some description", Color = Color.Green },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 6)), Color = Color.GreenYellow},
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 3)), Color = Color.LawnGreen},
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = "Some description", Color = Color.SkyBlue },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = string.Concat(Enumerable.Repeat($"Some description {Environment.NewLine}", 5)), Color = Color.LightCyan },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = "Some description", Color = Color.PaleTurquoise },
                new Car { Abbreviation = "VW", Make=CarMake.VolksWagen, Name = "Polo", Description = "Some description", Color = Color.Purple },
            };

            foreach (var car in cars)
            {
                car.CollapseCommand = new DelegateCommand(SetCarCollectables);
                if (allowDelete)
                    car.LongPressCommand = new DelegateCommand<Car>(AskRemoveCar);
            }

            Cars = cars;
        }

        private ObservableCollection<Car> _cars;

        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set { SetProperty(ref _cars, value); }
        }

        private bool _isToggling = false;
    }
}
