using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WindowsFront_end.Models.DTO_s;

namespace WindowsFront_end.Models
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
            set { _color = value; RaisePropertyChanged("Color"); }
        }

        private DateTime _start;
        public DateTime Start
        {
            get { return _start; }
            set { _start = value; RaisePropertyChanged("Start"); }
        }

        private DateTime _end;
        public DateTime End
        {
            get { return _end; }
            set { _end = value; RaisePropertyChanged("End"); }
        }
        private Route _route;
        public Route Route
        {
            get { return _route; }
            set { _route = value; RaisePropertyChanged("Route"); }
        }



        public List<Item> Items { get; set; } = new List<Item>();

        private List<Person> _travelers = new List<Person>();
        public List<Person> Travelers
        {
            get { return _travelers; }
            set { _travelers = value; RaisePropertyChanged("Travelers"); }
        }
        private List<Person> _invited = new List<Person>();
        public List<Person> Invited
        {
            get { return _invited; }
            set { _invited = value; RaisePropertyChanged("Invited"); }
        }
        public List<Category> Categories { get; set; } = new List<Category>();

        public Trip(int tripId, string name, string color, DateTime start, DateTime end)
        {
            TripId = tripId;
            Name = name;
            Color = color;
            Start = start;
            End = end;
            Route = new Route();

        }
        public Trip(TripDTO.Overview dto)
        {
            TripId = dto.TripId;
            Name = dto.Name;
            Color = dto.Color;
            Start = dto.Start;
            End = dto.End;

        }

        public Trip(TripDTO.Detail dto)
        {

            TripId = dto.TripId;
            Name = dto.Name;
            Color = dto.Color;
            Start = dto.Start;
            End = dto.End;
            Travelers = dto.Travelers.Select(t => new Person(t)).ToList();
            Invited = dto.Invited.Select(t => new Person(t)).ToList();
            Route = new Route(dto.Route);
            Items = dto.Items.Select(i => new Item(i)).ToList();
            Categories = dto.Categories.Select(c => new Category(c)).ToList();
        }

        public Trip()
        {
            Route = new Route();
        }
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}