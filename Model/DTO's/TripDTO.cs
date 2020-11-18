using System;
using System.Collections.Generic;

namespace WindowsBackend.Models
{
    public class TripDTO
    {

        public int TripId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<int> RouteIds { get; set; } = new List<int>();
        public List<int> ItemIds { get; set; } = new List<int>();
        public List<int> TravelerIds { get; set; } = new List<int>();

    }
}