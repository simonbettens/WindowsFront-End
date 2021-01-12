using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Models.DTO_s;

namespace WindowsFront_end.Models
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string PersonId { get; set; }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; RaisePropertyChanged("FirstName"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged("Email"); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged("Address"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged("Password"); }
        }

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set { _passwordConfirm = value; RaisePropertyChanged("PasswordConfirm"); }
        }
        public List<Trip> Trips { get; set; } = new List<Trip>();
        public List<Trip> Invites { get; set; } = new List<Trip>();
        public List<ItemDTO.ForPersonOverview> Items { get; set; } = new List<ItemDTO.ForPersonOverview>();

        public Person(string email, string password, string firstName, string name, string passwordConfirm, string address)
        {
            FirstName = firstName;
            Name = name;
            PasswordConfirm = passwordConfirm;
            Address = address;
            Email = email;
            Password = password;
        }

        public Person(PersonDTO.Overview dto)
        {
            FirstName = dto.FirstName;
            Name = dto.Name;
            PasswordConfirm = "";
            Address = dto.Address;
            Email = dto.Email;
            Password = "";
        }

        public Person(PersonDTO.FullOverview dto)
        {
            FirstName = dto.FirstName;
            Name = dto.Name;
            PasswordConfirm = "";
            Address = dto.Address;
            Email = dto.Email;
            Password = "";
            Trips = dto.Trips.Select(t => new Trip(t)).ToList();
            Invites = dto.Invited.Select(i => new Trip(i)).ToList();
            Items = dto.Items;
        }
        public Person()
        {

        }
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}