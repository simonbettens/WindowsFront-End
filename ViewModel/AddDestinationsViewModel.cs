using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WindowsFront_end.Models;

namespace WindowsFront_end.ViewModel
{
    public class AddDestinationsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Destination> DestinationsList { get; set; }

        private readonly string _defaultText = "Geen locatie geselecteerd";

        private Destination _destinationInMaking;

        public Destination DestinationInMaking
        {
            get { return _destinationInMaking; }
            set { _destinationInMaking = value; RaisePropertyChanged("DestinationInMaking"); }
        }

        private bool _validationSucces;
        public bool ValidationSucces
        {
            get { return _validationSucces; }
            set { _validationSucces = value; RaisePropertyChanged("ValidationSucces"); }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged("ErrorMessage"); }
        }

        private string _headerText;
        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; RaisePropertyChanged("HeaderText"); }
        }

        public RelayCommand RemoveDestinationCommand { get; set; }
        public RelayCommand SaveDestinationCommand { get; set; }
        public Trip Trip { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public AddDestinationsViewModel()
        {
            RemoveDestinationCommand = new RelayCommand((param) => RemoveDesination(param.ToString()));
            SaveDestinationCommand = new RelayCommand((param) => SaveDestination());
            DestinationInMaking = new Destination();
            DestinationsList = new ObservableCollection<Destination>();
            ValidationSucces = false;
            ErrorMessage = "";
            HeaderText = _defaultText;
            AddValidation();
        }


        public void SaveDestination()
        {
            this.DestinationsList.Add(DestinationInMaking);
            Trip.Route.Destinations.Add(DestinationInMaking);
            DestinationInMaking = new Destination();
            HeaderText = _defaultText;
            ValidationSucces = false;
            ErrorMessage = "";
            AddValidation();
        }
        public void InputLocation(double lat, double lon, string address)
        {
            DestinationInMaking.Latitude = lat;
            DestinationInMaking.Longitude = lon;
            DestinationInMaking.Address = address;
            DestinationInMaking.Name = address;
            HeaderText = address;
        }

        private void AddValidation()
        {
            this.DestinationInMaking.PropertyChanged += (sender, e) => ValidateDestination();
        }

        public void ValidateDestination()
        {
            if (DestinationInMaking.Name == null || DestinationInMaking.Name == "") { ValidationSucces = false; ErrorMessage = "Zoek op een address of klik op de kaart"; return; }
            if (DestinationInMaking.Description == null || DestinationInMaking.Description == "") { ValidationSucces = false; ErrorMessage = "Geef een beschrijving mee voor deze locatie"; return; }
            ValidationSucces = true;
            ErrorMessage = "";
        }

        public Destination[] GetDestinationsAsArray()
        {
            return DestinationsList.ToList().ToArray();
        }

        public void RemoveDesination(string sendByWho)
        {
            var dest = DestinationsList.Where(t => t.Address == sendByWho).FirstOrDefault();
            Trip.Route.Destinations.Remove(dest);
            DestinationsList.Remove(dest);
        }
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
