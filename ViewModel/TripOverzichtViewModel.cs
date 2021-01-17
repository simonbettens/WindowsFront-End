using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WindowsFront_end.Models;
using WindowsFront_end.Repository;

namespace WindowsFront_end.ViewModel
{
    /// <author>
    /// 
    /// </author>
    /// <summary>
    /// </summary>
    public class TripOverzichtViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Trip> TripList { get; set; }
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

        public TripOverzichtViewModel()
        {
            TripList = new ObservableCollection<Trip>();
            GotDataNotSuccesfull = false;
            IsBusy = true;
            LoadingDone = false;
            GetDataAsync();
        }

        private void GetData()
        {
            var polen = new Trip(1, "Polen", "#008a02", DateTime.Now, DateTime.Now.AddDays(5));
            TripList.Add(polen);
            var frankrijk = new Trip(2, "Frankrijk", "#d8e305", DateTime.Now, DateTime.Now.AddDays(6));
            TripList.Add(frankrijk);
            var wallonie = new Trip(3, "Wallonie", "#e30505", DateTime.Now, DateTime.Now.AddDays(7));
            TripList.Add(wallonie);
            var zweden = new Trip(4, "Zweden", "#63bef7", DateTime.Now, DateTime.Now.AddDays(5));
            TripList.Add(zweden);
            var singapore = new Trip(5, "Singapore", "#ba0ba6", DateTime.Now, DateTime.Now.AddDays(5));
            TripList.Add(singapore);
            GotDataNotSuccesfull = false;
        }
        private async void GetDataAsync()
        {
            try
            {
                //test
                //https://localhost:5001/api/Trip/GetAllTrips
                var list = await TripController.GetAllAsync();
                foreach (var trip in list)
                {
                    this.TripList.Add(trip);
                }
                GotDataNotSuccesfull = false;
            }
            catch (Exception)
            {
                GotDataNotSuccesfull = true;
            }
            IsBusy = false;
            LoadingDone = true;
        }

        public void Add(Trip newTrip)
        {
            TripList.Add(newTrip);
        }
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
