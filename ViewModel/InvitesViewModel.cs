using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsFront_end.Controllers;
using WindowsFront_end.Models;
using WindowsFront_end.Repository;

namespace WindowsFront_end.ViewModel
{
    public class InvitesViewModel : INotifyPropertyChanged
    {
        public ApplicationDataContainer LocalSettings { get; }
        public ObservableCollection<Trip> TripList { get; set; }

        private Person _person;
        public Person Person
        { 
            get { return _person; }
            set { _person = value; RaisePropertyChanged("Person"); } 
        }
      

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

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void getInvitedTrips()
        {
            
        }

        public InvitesViewModel(ApplicationDataContainer localSettings)
        {
            LocalSettings = localSettings;
            GetPerson(ApplicationData.Current.LocalSettings.Values["current_user_email"] as string);
            
        }

        private async void GetPerson(string email)
        {
           this.Person = await AccountController.GetPersonByEmail(email);
        }

        public async void AcceptInviteToTrip(int tripId)
        {
           var response =  await TripController.AcceptInviteToTrip(tripId);

        }

        public async void DeclineInviteToTrip(int tripId)
        {
            var response = await TripController.DeclineInviteToTrip(tripId);
        }

    }
}
