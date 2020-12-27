using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsBackend.Models;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Models;

namespace WindowsFront_end.Models.DTO_s
{
    public static class PersonDTO
    {


        public class Overview
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }

            public Overview(Person person)
            {
                Email = person.Email;
                FirstName = person.FirstName;
                Name = person.Name;
                Address = person.Address;
            }

        }

        public class OverviewWithItems
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public List<int> Trips { get; set; } = new List<int>();
            public List<ItemDTO.ForPersonOverview> Items { get; set; }

            public OverviewWithItems(Person person)
            {
                Email = person.Email;
                FirstName = person.FirstName;
                Name = person.Name;
                Address = person.Address;
                Trips = person.Trips.Select(t => t.TripId).ToList();
                Items = person.Items.Select(i => new ItemDTO.ForPersonOverview(i)).ToList();
            }
        }

        public class OverviewWithTrips
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public List<TripDTO.Overview> Trips { get; set; } = new List<TripDTO.Overview>();
        }


        public class Login
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class Register : Login
        {
            [Required]
            [StringLength(200)]
            public String FirstName { get; set; }

            [Required]
            [StringLength(250)]
            public String Name { get; set; }

            [Required]
            [Compare("Password")]
            [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
            public String PasswordConfirmation { get; set; }

            [Required]
            public String Adress { get; set; }
        }

    }
    }
