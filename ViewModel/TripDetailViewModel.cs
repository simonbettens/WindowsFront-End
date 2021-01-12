using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using WindowsFront_end.Models;
using WindowsFront_end.Repository;

namespace WindowsFront_end.ViewModel
{
    public class TripDetailViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged("IsBusy"); }
        }
        private bool _gotDataNotSuccesfull;
        public bool GotDataNotSuccesfull
        {
            get { return _gotDataNotSuccesfull; }
            set { _gotDataNotSuccesfull = value; RaisePropertyChanged("GotDataNotSuccesfull"); }
        }
        private bool _loadingDone;
        public bool LoadingDone
        {
            get { return _loadingDone; }
            set { _loadingDone = value; RaisePropertyChanged("LoadingDone"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Trip _trip;
        public Trip Trip
        {
            get { return _trip; }
            set { _trip = value; RaisePropertyChanged("Trip"); }
        }

        private List<Person> _travelers;
        public List<Person> Travelers
        {
            get { return _travelers; }
            set { _travelers = value; RaisePropertyChanged("Travelers"); }
        }

        private List<Item> _toDoList;
        public List<Item> ToDoList
        {
            get { return _toDoList; }
            set { _toDoList = value; RaisePropertyChanged("ToDoList"); }
        }

        //return Trip != null? Trip.Items.Where(i => i.ItemType == ItemType.ToDo).ToList(): null;

        private List<Item> _toPackList;
        public List<Item> ToPackList
        {
            get { return _toPackList; }
            set { _toPackList = value; RaisePropertyChanged("ToPackList"); }
        }

        public string ShareString { get; set; }

        public TripDetailViewModel()
        {
            GetTripAsync(1);
        }

        public void BuildShareString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Hey ik ga op reis\n");
            builder.Append($"Ik vertrek op {Trip.Start.ToString("dd-MM-yy")} en kom terug om {Trip.End.ToString("dd-MM-yy")}\n");
            builder.Append("Ik bezoek deze plaatsen\n");
            foreach (var destination in Trip.Route.Destinations)
            {
                builder.Append($" - {destination.Name} \n");
            }
            ShareString = builder.ToString();
        }

        public void BuildShareStringHTML()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<h1>Hey ik ga op reis</h1>");
            builder.Append($"<h3>Ik vertrek op {Trip.Start.ToString("dd-MM-yy")} en kom terug om {Trip.End.ToString("dd-MM-yy")}</h3>");
            builder.Append("<p>Ik bezoek deze plaatsen</p>");
            builder.Append("<ol>");
            foreach (var destination in Trip.Route.Destinations)
            {
                builder.Append($"<li>{destination.Name}</li>");
            }
            builder.Append("</ol>");
            ShareString = builder.ToString();
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void GetTripAsync(int tripId)
        {
            try
            {
                Trip trip = await TripController.GetTripAsync(tripId);
                Trip = trip;
                ToDoList = Trip.Items.Where(i => i.ItemType == ItemType.ToDo).ToList();
                ToPackList = Trip.Items.Where(i => i.ItemType == ItemType.ToPack).ToList();
                GotDataNotSuccesfull = false;
                BuildShareString();
            }
            catch
            {
                GotDataNotSuccesfull = true;
            }
            IsBusy = false;
            LoadingDone = true;
        }

    }
}
