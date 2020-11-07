using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using WindowsFront_end.Model;
using WindowsFront_end.Util;

namespace WindowsFront_end.ViewModel
{
    public class TripOverzichtViewModel
    {
        public ObservableCollection<Trip> TripList { get; set; }

        public TripOverzichtViewModel()
        {
            TripList = new ObservableCollection<Trip>();
            //GetData();
            GetDataAsync();
        }

        private void GetData()
        {


            var polen = new Trip("Polen", "#008a02", DateTime.Now, DateTime.Now.AddDays(5));
            TripList.Add(polen);
            var frankrijk = new Trip("Frankrijk", "#d8e305", DateTime.Now, DateTime.Now.AddDays(6));
            TripList.Add(frankrijk);
            var wallonie = new Trip("Wallonie", "#e30505", DateTime.Now, DateTime.Now.AddDays(7));
            TripList.Add(wallonie);
            var zweden = new Trip("Zweden", "#63bef7", DateTime.Now, DateTime.Now.AddDays(5));
            TripList.Add(zweden);
            var singapore = new Trip("Singapore", "#ba0ba6", DateTime.Now, DateTime.Now.AddDays(5));
            TripList.Add(singapore);
        }
        private async void GetDataAsync()
        {
            HttpClient client = new HttpClient();
            var json = "";
            try
            {
                //https://localhost:5001/api/Trip/GetAllTrips
                json = await client.GetStringAsync(new Uri(UrlUtil.PorjectURL + "Trip/GetAllTrips"));

            }
            catch (Exception e)
            {

                throw e;
            }
            ///https://localhost:44372/api/Trip/GetAllTrips
            var lst = JsonConvert.DeserializeObject<List<Trip>>(json);
            foreach (Trip trip in lst)
            {
                this.TripList.Add(trip);
            }
        }

        public void Add()
        {


            var test = new Trip("test", "#81fcd9", DateTime.Now, DateTime.Now.AddDays(5));
            TripList.Add(test);

        }
    }
}
