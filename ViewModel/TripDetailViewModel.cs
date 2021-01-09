using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WindowsBackend.Models.DTO_s;
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
            try
            {
                Trip trip = await TripController.GetTripAsync(tripId);
                Trip = trip;
                ToDoList = Trip.Items.Where(i => i.ItemType == ItemType.ToDo).ToList();
                ToPackList = Trip.Items.Where(i => i.ItemType == ItemType.ToPack).ToList();
                GotDataNotSuccesfull = false;
            }
            catch
            {
                GotDataNotSuccesfull = true;
            }
            IsBusy = false;
            LoadingDone = true;
        }

        public async Task<bool> AddItemAsync(ItemDTO.Create item)
        {
            var loginJson = JsonConvert.SerializeObject(item);

            HttpClient client = new HttpClient();
            var data = new StringContent(loginJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                //https://localhost:5001/trip/${Trip.tripId}/item
                response = await client.PostAsync(new Uri(UrlUtil.ProjectURL + $"trip/{Trip.TripId}/item"),
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

    }
}
