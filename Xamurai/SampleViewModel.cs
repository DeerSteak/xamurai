using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xamurai
{
    public class SampleViewModel : BindableBase
    {
        public SampleViewModel()
        {
            GridSpan = Device.Idiom == TargetIdiom.Phone ? 1 : 2;
            BuildCars();
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

            //column is half of screen width but remove 10px on either side
            //for margin; two columns so three total margins
            ColumnWidth = (int)(screenWidth - 30) / 2;
        }

        private void SetColumnHeight()
        {
            //get screen height in points
            double screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;

            //quick and dirty account for status bar and navigation bar
            var navBarHeight = DeviceInfo.Platform == DevicePlatform.iOS ? 100 : 50;
            ColumnHeight = (int)(screenHeight - navBarHeight);
        }

        private ObservableCollection<CarCollectable> _carCollectables;

        public ObservableCollection<CarCollectable> CarCollectables
        {
            get => _carCollectables;
            set => SetProperty(ref _carCollectables, value);
        }

        public void SetCarCollectables()
        {
            SetColumnHeight();
            SetColumnWidth();

            var collectables = new ObservableCollection<CarCollectable>();
            var collectable = GetNewCollectable();
            var currentHeight = 0;
            var sizeConv = new FontSizeConverter();
            var textSize = (double)sizeConv.ConvertFromInvariantString("Default") * DeviceDisplay.MainDisplayInfo.Density;

            foreach (var car in Cars)
            {
                //TODO: re-flowing the layout is janky on iOS. 

                var split = car.Description.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                var descLines = split.Length;
                var cardHeight = 50;
                if (car.IsVisible)
                    cardHeight += (int)(textSize * descLines);
                currentHeight += cardHeight;
                if (currentHeight > ColumnHeight * 2)
                {
                    collectables.Add(collectable);
                    collectable = GetNewCollectable();
                    currentHeight = cardHeight;
                }
                collectable.CollectedCars.Add(car);
            }
            collectables.Add(collectable);

            //workaround for Android display blanks on reflow
            CarCollectables = null;

            CarCollectables = collectables;
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

        private void BuildCars()
        {
            Cars = new ObservableCollection<Car>
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

            foreach (var car in Cars)
            {
                car.LongPressCommand = new Command<Car>(AskRemoveCar);
                car.IsVisible = true;
                car.WasToggled = SetCarCollectables;
            }
        }

        private ObservableCollection<Car> _cars;

        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set { SetProperty(ref _cars, value); }
        }
    }
}
