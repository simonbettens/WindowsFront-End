using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WindowsFront_end.Models;

namespace WindowsFront_end.ViewModel
{
    public class AddTripViewModel : INotifyPropertyChanged
    {
        public Trip Trip { get; set; }
        private bool _areFieldsValid;
        public bool AreFieldsValid
        {
            get { return _areFieldsValid; }
            set { _areFieldsValid = value; RaisePropertyChanged("AreFieldsValid"); }
        }


        private string _erroMessage;
        public string ErrorMessage
        {
            get { return _erroMessage; }
            set { _erroMessage = value; RaisePropertyChanged("ErrorMessage"); }
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; RaisePropertyChanged("IsOpen"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AddTripViewModel()
        {
            Trip = new Trip();
            AreFieldsValid = false;
        }


        /// <summary>
        /// checks if all fields are valid (filled in) (could use validators here)
        /// </summary>
        /// <returns> true if correct, false if not</returns>
        public void CheckAreFieldValid()
        {
            if (Trip.Name == null || Trip.Name.Equals("")) { AreFieldsValid = false; IsOpen = true; ErrorMessage = "Naam van de trip is niet ingevuld"; return; }
            if (Trip.Start == null || Trip.Start == DateTime.MinValue) { AreFieldsValid = false; IsOpen = true; ErrorMessage = "Er moet een startdatum zijn"; return; }
            if (Trip.End == null || Trip.End == DateTime.MinValue) { AreFieldsValid = false; IsOpen = true; ErrorMessage = "Er moet een einddatum zijn"; return; }
            if (Trip.Route.Description == null || Trip.Route.Description.Equals("")) { AreFieldsValid = false; IsOpen = true; ErrorMessage = "Er is nog geen beschrijving voor deze trip"; return; }
            if (Trip.Color == null || Trip.Color.Equals("")) { AreFieldsValid = false; IsOpen = true; ErrorMessage = "Er is nog geen kleur voor deze trip"; return; }
            AreFieldsValid = true;
            IsOpen = false; ;
            ErrorMessage = "";

        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
