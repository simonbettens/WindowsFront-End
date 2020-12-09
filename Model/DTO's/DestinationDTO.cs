using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

{
    public static class DestinationDTO
    {
        public class Overview
        {
            public int DestinationId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public Overview(Destination destination)
            {
                DestinationId = destination.DestinationId;
                Name = destination.Name;
                Description = destination.Description;
                Address = destination.Address;
                Latitude = destination.Latitude;
                Longitude = destination.Longitude;
            }

        }

        public class Create
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

    }
}
