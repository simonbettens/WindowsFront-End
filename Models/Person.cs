using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;

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
            set { _firstName = value; RaisePropertyChanged("PersonFirstName"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("PersonName"); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged("PersonEmail"); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged("PersonAddress"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged("PersonPassword"); }
        }

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set { _passwordConfirm = value; RaisePropertyChanged("PersonPasswordConfirm"); }
        }
        public List<Trip> Trips { get; set; } = new List<Trip>();
        public List<ItemPerson> Items { get; set; } = new List<ItemPerson>();

        public Person(string email, string password, string firstName, string name, string passwordConfirm, string address)
        {
            FirstName = firstName;
            Name = name;
            PasswordConfirm = passwordConfirm;
            Address = address;
            Email = email;
            Password = password;
        }

        public Person(TextBox naam, TextBox voornaam)
        {

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