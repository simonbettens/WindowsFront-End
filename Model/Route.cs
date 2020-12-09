using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsFront_end.Model
{
    public class Route : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int RouteId { get; set; }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged("RouteDescription"); }
        }
        public List<Destination> Destinations { get; set; } = new List<Destination>();
        public Trip Trip { get; set; }


        public Route(string description)
        {
            Description = description;
        }

        public Route()
        {

        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}