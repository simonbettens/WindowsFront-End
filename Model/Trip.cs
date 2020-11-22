using System;
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

        private string _color;
        public string Color
        {
            get { return _color; }
            set { _color = value; RaisePropertyChanged("TripColor"); }
        }

        private DateTime _start;
        public DateTime Start
        {
            get { return _start; }
            set { _start = value; RaisePropertyChanged("TripStart"); }
        }

        private DateTime _end;
        public DateTime End
        {
            get { return _end; }
            set { _end = value; RaisePropertyChanged("TripEnd"); }
        }
        public List<Route> Routes { get; set; } = new List<Route>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Trip> Travelers { get; set; } = new List<Trip>();

        public Trip(string name, string color, DateTime start, DateTime end)
        {
            Name = name;
            Color = color;
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