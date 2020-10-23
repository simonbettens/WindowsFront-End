using System.Collections.Generic;

namespace WindowsFront_end.Model
{
    public class Destination
    {
        public int DestinationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public List<DestinationRoute> Routes { get; set; } = new List<DestinationRoute>();

        public Destination(string name, string description, string address)
        {
            Name = name;
            Description = description;
            Address = address;
        }

        public Destination()
        {
        }
    }
}