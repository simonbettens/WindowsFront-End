using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WindowsFront_end.Models.DTO_s;

namespace WindowsFront_end.Model
{
    public class Trip : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _tripId;
        public int TripId
        {
            get { return _tripId; }
            set { _tripId = value; RaisePropertyChanged("TripId"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
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
        public Route Route { get; set; } = new Route();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<TravelerTrip> Travelers { get; set; } = new List<TravelerTrip>();

        public Trip(int tripId, string name, string color, DateTime start, DateTime end)
        {
            TripId = tripId;
            Name = name;
            Color = color;
            Start = start;
            End = end;

        }
        public Trip(TripDTO.Overview dto)
        {
            Name = dto.Name;
            Color = dto.Color;
            Start = dto.Start;
            End = dto.End;

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