using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsBackend.Models;
using WindowsBackend.Models.DTO_s;

namespace WindowsFront_end.Model.DTO_s
{
    public static class PersonDTO
    {


        public class Overview
        {
            public string Email { get; set; }
            public string SurName { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }

            public Overview(Person person)
            {
                Email = person.Email;
                SurName = person.SurName;
                Name = person.Name;
                Address = person.Address;
            }

        }

        public class OverviewWithItems
        {
            public string Email { get; set; }
            public string SurName { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public List<int> Trips { get; set; } = new List<int>();
            public List<ItemDTO.ForPersonOverview> Items { get; set; }

            public OverviewWithItems(Person person)
            {
                Email = person.Email;
                SurName = person.SurName;
                Name = person.Name;
                Address = person.Address;
                Trips = person.Trips.Select(t => t.TripId).ToList();
                Items = person.Items.Select(i => new ItemDTO.ForPersonOverview(i)).ToList();
            }
        }

        public class OverviewWithTrips
        {
            public string Email { get; set; }
            public string SurName { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public List<TripDTO.Overview> Trips { get; set; } = new List<TripDTO.Overview>();
        }
    }
    }
