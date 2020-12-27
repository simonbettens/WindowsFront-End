using System.Collections.ObjectModel;
using System.Linq;
using WindowsFront_end.Models;

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
            DestinationInMaking = new Destination();
        }
        public void InputLocation(double lat, double lon, string address)
        {
            DestinationInMaking.Latitude = lat;
            DestinationInMaking.Longitude = lon;
            DestinationInMaking.Address = address;
        }
        public Destination[] GetDestinationsAsArray()
        {
            return DestinationsList.ToList().ToArray();
        }
    }
}
