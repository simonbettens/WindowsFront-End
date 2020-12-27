using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsFront_end.Models
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
        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; RaisePropertyChanged("Latitude"); }
        }
        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; RaisePropertyChanged("Longitude"); }
        }

        public Destination(string name, string description, string address, double latitude, double longitude)
        {
            Name = name;
            Description = description;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
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