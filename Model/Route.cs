using System.Collections.Generic;

namespace WindowsFront_end.Model
{
    public class Route
    {
        public int RoutId { get; set; }
        public string Description { get; set; }
        public List<DestinationRoute> Destinations { get; set; } = new List<DestinationRoute>();
        public Trip Trip { get; set; }


        public Route(string description)
        {
            Description = description;
        }

        public Route()
        {
        }
    }
}