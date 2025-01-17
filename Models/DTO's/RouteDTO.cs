﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFront_end.Models.DTO_s;

namespace WindowsFront_end.Models.DTO_s
{
    public static class RouteDTO
    {

        public class Overview
        {
            public int RouteId { get; set; }
            public string Description { get; set; }
            public List<DestinationDTO.Overview> Destinations { get; set; } = new List<DestinationDTO.Overview>();

            public Overview()
            {

            }

            public Overview(Route route)
            {
                RouteId = route.RouteId;
                Description = route.Description;
                Destinations = route.Destinations.Select(d => new DestinationDTO.Overview(d)).ToList();
            }
        }

        public class Create
        {
            public string Description { get; set; }
            public List<DestinationDTO.Create> Destinations { get; set; } = new List<DestinationDTO.Create>();
        }




    }
}
