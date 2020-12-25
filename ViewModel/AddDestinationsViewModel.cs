using System.Collections.ObjectModel;
using WindowsFront_end.Model;

namespace WindowsFront_end.ViewModel
{
    public class AddDestinationsViewModel
    {
        public ObservableCollection<Destination> DestinationsList { get; set; }
        public Destination DestinationInMaking { get; set; }
        public AddDestinationsViewModel()
        {
            DestinationInMaking = new Destination();
            DestinationsList = new ObservableCollection<Destination>();
        }
        public void SaveDestination()
        {
            this.DestinationsList.Add(DestinationInMaking);
        }
        public void InputLocation(double lat, double lon, string address)
        {
            DestinationInMaking.Latitude = lat;
            DestinationInMaking.Longitude = lon;
            DestinationInMaking.Address = address;
        }
    }
}
