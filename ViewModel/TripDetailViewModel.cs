using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using WindowsFront_end.Model;
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


        public List<Item> ToDoList
        {
            get { return _trip.Items.Where(i => i.ItemType == ItemType.ToDo).ToList(); }
        }



        public List<Item> ToPackList
        {
            get { return _trip.Items.Where(i => i.ItemType == ItemType.ToPack).ToList(); }
        }




        public TripDetailViewModel()
        {
            GetTripAsync(1);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void GetTripAsync(int tripId)
        {
            HttpClient client = new HttpClient();
            var json = "";
            try
            {
                json = await client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"trip/{tripId}"));
                GotDataNotSuccesfull = false;
            }
            catch
            {
                GotDataNotSuccesfull = true;
            }

            if(!GotDataNotSuccesfull)
            {
                this.Trip = JsonConvert.DeserializeObject<Trip>(json);
                GetItemsAsync();
            }
            IsBusy = false;
            LoadingDone = true;
        }

        public async void GetItemsAsync()
        {
            HttpClient client = new HttpClient();
            var json = "";
            try
            {
                json = await client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"trip/{Trip.TripId}/item"));
                GotDataNotSuccesfull = false;
            }
            catch
            {
                GotDataNotSuccesfull = true;
            }

            if (!GotDataNotSuccesfull)
            {
                var lst = JsonConvert.DeserializeObject<List<Item>>(json);
                foreach (Item item in lst)
                {
                    if (item.ItemType ==  ItemType.ToDo)
                    {
                        this.ToDoList.Add(item);
                    }
                    else
                    {
                        this.ToPackList.Add(item);
                    }
                    
                }
            }
        }

    }
}
