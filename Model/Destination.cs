using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsFront_end.Model
{
    public class Destination : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int DestinationId { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("DestinationName"); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged("DestinationDescription"); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged("DestinationAddress"); }
        }

        public List<Route> Routes { get; set; } = new List<Route>();

        public Destination(string name, string description, string address)
        {
            Name = name;
            Description = description;
            Address = address;
        }

        public Destination()
        {

        }


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}