﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsFront_end.Controllers;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Repository;

namespace WindowsFront_end.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set { _person = value; RaisePropertyChanged("Person"); }
        }

        public bool HasInvites { get; set; } = false;
        public bool HasNoInvites { get; set; } = true;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged("ErrorMessage"); }
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
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged("IsBusy"); }
        }
        public RelayCommand UpdateProfileCommand { get; set; }
        private readonly string _currentuser;
        private readonly ApplicationDataContainer _localSettings;

        public ProfileViewModel(string currentuser, ApplicationDataContainer localSettings)
        {
            Person = new Person();
            UpdateProfileCommand = new RelayCommand((param) => UpdateProfile());
            LoadingDone = false;
            _currentuser = currentuser;
            this._localSettings = localSettings;
            GotDataNotSuccesfull = false;
            IsBusy = true;
            GetProfile(currentuser);
        }

        private async void GetProfile(string currentuser)
        {
            try
            {
                Person = await AccountController.GetPersonByEmail(currentuser);
                if (Person.Invites.Count > 0)
                {
                    HasInvites = true;
                    RaisePropertyChanged("HasInvites");
                    HasNoInvites = false;
                    RaisePropertyChanged("HasNoInvites");
                }
                else
                {
                    HasInvites = false;
                    RaisePropertyChanged("HasInvites");
                    HasNoInvites = true;
                    RaisePropertyChanged("HasNoInvites");
                }
                GotDataNotSuccesfull = false;
                ErrorMessage = "";
            }
            catch (Exception)
            {
                GotDataNotSuccesfull = true;
                ErrorMessage = "Kon profile niet opvragen";
            }
            LoadingDone = true;
            IsBusy = false;
        }

        private async void UpdateProfile()
        {
            try
            {
                var data = new PersonDTO.Overview(Person);
                var response = await AccountController.UpdatePersonByEmail(_currentuser, data);

                if (response.IsSuccessStatusCode)
                {
                    _localSettings.Values["current_user_email"] = Person.Email;
                    ErrorMessage = "";
                    GotDataNotSuccesfull = false;
                }
                else
                {
                    ErrorMessage = "Kon profile niet updaten";
                    GotDataNotSuccesfull = true;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Kon profile niet updaten";
                GotDataNotSuccesfull = true;
            }
        }

        public async Task AcceptInvite(int tripId)
        {
            var response = await TripController.AcceptInviteToTrip(tripId);
            if (response.IsSuccessStatusCode)
            {
                GetProfile(_currentuser);
            }

        }

        public async Task DeclineInvite(int tripId)
        {
            var response = await TripController.DeclineInviteToTrip(tripId);
            if (response.IsSuccessStatusCode)
            {
                GetProfile(_currentuser);
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
