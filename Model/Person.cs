using System.Collections.Generic;

namespace WindowsFront_end.Model
{
    public class Person
    {
        public string PersonId { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<TravelerTrip> Trips { get; set; } = new List<TravelerTrip>();

        public Person(string sirName, string name, string email, string address)
        {
            SurName = sirName;
            Name = name;
            Email = email;
            Address = address;
        }

        public Person()
        {
        }
    }
}