using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Util;

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
            if (Trip.Name == null || Trip.Name.Equals("")) { AreFieldsValid = false; ErrorMessage = "Naam van de trip is niet ingevuld"; return; }
            if (Trip.Start == null || Trip.Start == DateTime.MinValue) { AreFieldsValid = false; ErrorMessage = "Er moet een startdatum zijn"; return; }
            if (Trip.End == null || Trip.End == DateTime.MinValue) { AreFieldsValid = false; ErrorMessage = "Er moet een einddatum zijn"; return; }
            if (Trip.Route.Description == null || Trip.Route.Description.Equals("")) { AreFieldsValid = false; ErrorMessage = "Er is nog geen beschrijving voor deze trip"; return; }
            if (Trip.Color == null || Trip.Color.Equals("")) { AreFieldsValid = false; ErrorMessage = "Er is nog geen kleur voor deze trip"; return; }
            AreFieldsValid = true;
            ErrorMessage = "";

        }
        /// <summary>
        /// (Test method) (should happen later)
        /// Save and sends Trip to backend 
        /// </summary>
        public async void Save()
        {
            HttpClient client = new HttpClient();
            TripDTO.Create tripDTO = new TripDTO.Create
            {
                Name = Trip.Name,
                Color = Trip.Color,
                End = Trip.End,
                Start = Trip.Start,
                Items = Trip.Items.Select(i => new ItemDTO.Overview(i)).ToList(),
                Route = new RouteDTO.Overview(Trip.Route)
            };
            var json = JsonConvert.SerializeObject(tripDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                return;
                //https://localhost:5001/api/Trip/GetAllTrips
                response = await client.PostAsync(new Uri(UrlUtil.ProjectURL + "trip"), data);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Gelukt");
                }
                else
                {
                    Console.WriteLine("Failed");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
