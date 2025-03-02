﻿using System;
using System.Windows.Input;
using Prism.Mvvm;
using Xamarin.Forms;

namespace Xamurai
{
    public class Car : BindableBase
    {
        private string _abbreviation;

        public string Abbreviation
        {
            get { return _abbreviation; }
            set { SetProperty(ref _abbreviation, value); }
        }

        private CarMake _make;

        public CarMake Make
        {
            get { return _make; }
            set { SetProperty(ref _make, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }

        private Color _color;

        public Color Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        //added for PagedCollectionPage challenge

        private bool _isVisible = true;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetProperty(ref _isVisible, value);
            }
        }

        private ICommand _longPressCommand;

        public ICommand LongPressCommand
        {
            get => _longPressCommand;
            set => SetProperty(ref _longPressCommand, value);
        }

        private ICommand _collapseCommand;

        public ICommand CollapseCommand
        {
            get => _collapseCommand;
            set => SetProperty(ref _collapseCommand, value);
        }
    }

    public enum CarMake
    {
        VolksWagen,
        Mercedes,
        BMW
    }
}
