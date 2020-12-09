using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using WindowsBackend.Models;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Model;
using WindowsFront_end.Model.DTO_s;
using WindowsFront_end.Util;

namespace WindowsFront_end.ViewModel
{
    public class AddTripViewModel
    {
        public Trip Trip { get; set; }
        public bool AreFieldsValid { get; set; }

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
            if (Trip.Name == null || Trip.Name.Equals("")) { AreFieldsValid = false; return; }
            if (Trip.Color == null || Trip.Color.Equals("")) { AreFieldsValid = false; return; }
            if (Trip.Start == null) { AreFieldsValid = false; return; }
            if (Trip.End == null) { AreFieldsValid = false; return; }
            AreFieldsValid = true;
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

    }
}
