using System;
using System.Collections.Generic;

namespace WindowsFront_end.Model
{
    public class Trip
    {
        public int TripId { get; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<Route> Routes { get; set; } = new List<Route>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<TravelerTrip> Travelers { get; set; } = new List<TravelerTrip>();

        public Trip(string name, DateTime start, DateTime end)
        {
            Name = name;
            Start = start;
            End = end;

        }

        public Trip()
        {
        }
    }
}