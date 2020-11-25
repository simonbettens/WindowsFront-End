using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.System;

namespace WindowsFront_end.Model
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string PersonId { get; set; }

        private string _surName;
        public string SurName
        {
            get { return _surName; }
            set { _surName = value; RaisePropertyChanged("PersonSurName"); }
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
        public List<Trip> Trips { get; set; } = new List<Trip>();

        public Person(string sirName, string name, string email, string address)
        {
            SurName = sirName;
            Name = name;
            Email = email;
            Address = address;
        }

        public Person(Windows.UI.Xaml.Controls.TextBox naam, Windows.UI.Xaml.Controls.TextBox voornaam)
        {

        }
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}