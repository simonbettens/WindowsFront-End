using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Models;
using WindowsFront_end.Repository;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Util;

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

        private List<string> _categories;
        public List<string> Categories
            {
                get { return _categories; }
                set { _categories = value; RaisePropertyChanged("Categories"); }
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
                TripDTO.Detail trip = await TripController.GetTripAsync(tripId);
                Trip = new Trip(trip);
                this.Categories = Trip.Categories.Select(c => c.Name).ToList();
                this.Travelers = Trip.Travelers;
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

        public async Task<bool> AddItemAsync(ItemDTO.Create item,int tripId)
        {
            var loginJson = JsonConvert.SerializeObject(item);

            HttpClient client = new HttpClient();
            var data = new StringContent(loginJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                //https://localhost:5001/trip/${tripId}/item
                response = await client.PostAsync(new Uri(UrlUtil.ProjectURL + $"trip/{tripId}/item"),
                   data);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO.Create item)
        {
            var loginJson = JsonConvert.SerializeObject(item);

            HttpClient client = new HttpClient();
            var data = new StringContent(loginJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                //https://localhost:5001/trip/${Trip.tripId}/category
                response = await client.PostAsync(new Uri(UrlUtil.ProjectURL + $"trip/{Trip.TripId}/category"),
                   data);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            try
            {
                //https://localhost:5001/item/{id}
                response = await client.DeleteAsync(new Uri(UrlUtil.ProjectURL + $"item/{id}"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> MarkItemAsDoneOrNotDone(int itemId, string email)
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            var data = new StringContent("", Encoding.UTF8, "application/json");
            try
            {
                //https://localhost:5001/item/{id}
                response = await client.PutAsync(new Uri(UrlUtil.ProjectURL + $"trip/item/{itemId}/{email}/mark-as-done"),data);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
