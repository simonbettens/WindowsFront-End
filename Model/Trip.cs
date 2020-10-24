﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsFront_end.Model
{
    public class Trip : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int TripId { get; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("TripName"); }
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<Route> Routes { get; set; } = new List<Route>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Trip> Travelers { get; set; } = new List<Trip>();

        public Trip(string name, DateTime start, DateTime end)
        {
            Name = name;
            Start = start;
            End = end;

        }

        public Trip()
        {

        }
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}