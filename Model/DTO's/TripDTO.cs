using System;
using System.Collections.Generic;
using WindowsBackend.Models.DTO_s;

namespace WindowsFront_end.Models.DTO_s
{
    public static class TripDTO
    {
        public class Overview
        {
            public int TripId { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public class Create
        {
            public string Name { get; set; }
            public string Color { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public RouteDTO.Overview Route { get; set; }
            public List<ItemDTO.Overview> Items { get; set; } = new List<ItemDTO.Overview>();
            public List<PersonDTO.Overview> Travelers { get; set; } = new List<PersonDTO.Overview>();
        }

        public class Detail
        {
            public int TripId { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public RouteDTO.Overview Route { get; set; }
            public List<ItemDTO.Overview> Items { get; set; } = new List<ItemDTO.Overview>();
            public List<PersonDTO.Overview> Travelers { get; set; } = new List<PersonDTO.Overview>();
        }



    }
}