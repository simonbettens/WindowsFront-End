using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Controllers;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Repository;

namespace WindowsFront_end.ViewModel
{
    public class TripDetailViewModel : INotifyPropertyChanged
    {
        private int _toDoTotal;
        private int _toDoCurrent;
        private int _toDoProgress;
        public int ToDoProgress
        {
            get { return _toDoProgress; }
            set { _toDoProgress = value; RaisePropertyChanged("ToDoProgress"); }
        }
        private string _toDoProgressFormat;
        public string ToDoProgressFormat
        {
            get { return _toDoProgressFormat; }
            set { _toDoProgressFormat = value; RaisePropertyChanged("ToDoProgressFormat"); }
        }
        private int _toPackTotal;
        private int _toPackCurrent;
        private int _toPackProgress;
        public int ToPackProgress
        {
            get { return _toPackProgress; }
            set { _toPackProgress = value; RaisePropertyChanged("ToPackProgress"); }
        }
        private string _toPackProgressFormat;
        public string ToPackProgressFormat
        {
            get { return _toPackProgressFormat; }
            set { _toPackProgressFormat = value; RaisePropertyChanged("ToPackProgressFormat"); }
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged("ErrorMessage"); }
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

        private string _inviteEmail;
        public string InviteEmail
        {
            get { return _inviteEmail; }
            set { _inviteEmail = value; RaisePropertyChanged("InviteEmail"); }
        }
        /*
        private List<Item> _toDoList;
        public List<Item> ToDoList
        {
            get { return _toDoList; }
            set { _toDoList = value; RaisePropertyChanged("ToDoList"); }
        }*/
        public ObservableCollection<ItemDTO.ForOnePersonOverview> ToDoList { get; set; }
        public ObservableCollection<ItemDTO.ForOnePersonOverview> ToPackList { get; set; }
        //return Trip != null? Trip.Items.Where(i => i.ItemType == ItemType.ToDo).ToList(): null;
        /*
        private List<Item> _toPackList;
        public List<Item> ToPackList
        {
            get { return _toPackList; }
            set { _toPackList = value; RaisePropertyChanged("ToPackList"); }
        }*/

        private List<string> _categories;
        public List<string> Categories
        {
            get { return _categories; }
            set { _categories = value; RaisePropertyChanged("Categories"); }
        }
        public string ShareString { get; set; }

        public RelayCommand ModifyItemCommand { get; set; }


        public TripDetailViewModel()
        {
            //ModifyItemCommand = new RelayCommand(async (param) => await ModifyItem((ItemDTO.ForOnePersonOverview)param));
            ToDoList = new ObservableCollection<ItemDTO.ForOnePersonOverview>();
            ToPackList = new ObservableCollection<ItemDTO.ForOnePersonOverview>();
            _toDoCurrent = 0;
            _toDoTotal = 0;
            _toPackCurrent = 0;
            _toPackTotal = 0;
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

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void GetTripAsync(int tripId)
        {
            try
            {
                _toDoCurrent = 0;
                _toDoTotal = 0;
                _toPackCurrent = 0;
                _toPackTotal = 0;
                var email = (string)ApplicationData.Current.LocalSettings.Values["current_user_email"];
                TripDTO.Detail trip = await TripController.GetTripAsync(tripId);
                Trip = new Trip(trip);
                this.Categories = Trip.Categories.Select(c => c.Name).ToList();
                this.Travelers = Trip.Travelers;
                ToDoList.Clear();
                ToPackList.Clear();
                var toDoList = Trip.Items.Where(i => i.ItemType == ItemType.ToDo).ToList();
                toDoList.ForEach(i =>
                {
                    
                    var person = i.Persons.FirstOrDefault(p => p.PersonEmail.ToLower() == email.ToLower());
                    if (person != null)
                    {
                        var amount = i.Persons.Where(p => p.IsDone != true).ToList().Count();
                        var forOnePersonOverview = new ItemDTO.ForOnePersonOverview(i.ItemId, i.Name, i.ItemType, i.Category.Name, amount, person);
                        forOnePersonOverview.PropertyChanged += async (sender, e) => await UpdateItemAsync((ItemDTO.ForOnePersonOverview)sender);
                        ToDoList.Add(forOnePersonOverview);
                    }
                    _toDoTotal += i.Persons.Count();
                    _toDoCurrent += i.Persons.Where(p => p.IsDone == true).ToList().Count();

                });
                decimal toDoPercent = ((decimal)_toDoCurrent / (decimal)_toDoTotal) * 100;
                ToDoProgress = Convert.ToInt32(toDoPercent);
                ToDoProgressFormat = $"{ToDoProgress}%";

                var toPackList = Trip.Items.Where(i => i.ItemType == ItemType.ToPack).ToList();
                toPackList.ForEach(i =>
                {
                    var person = i.Persons.FirstOrDefault(p => p.PersonEmail.ToLower() == email.ToLower());
                    if (person != null)
                    {
                        var amount = i.Persons.Where(p => p.IsDone != true).ToList().Count();
                        var forOnePersonOverview = new ItemDTO.ForOnePersonOverview(i.ItemId, i.Name, i.ItemType, i.Category.Name, amount, person);
                        forOnePersonOverview.PropertyChanged += async (sender, e) => await UpdateItemAsync((ItemDTO.ForOnePersonOverview)sender);
                        ToPackList.Add(forOnePersonOverview);
                    }
                    _toPackTotal += i.Persons.Count();
                    _toPackCurrent += i.Persons.Where(p => p.IsDone == true).ToList().Count();
                });
                decimal toPackPercent = ((decimal)_toPackCurrent / (decimal)_toPackTotal) * 100;
                ToPackProgress = Convert.ToInt32(toPackPercent);
                ToPackProgressFormat = $"{ToPackProgress}%";
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

        public async Task<bool> AddItemAsync(ItemDTO.Create item, int tripId)
        {
            HttpResponseMessage response;

            //https://localhost:5001/trip/${tripId}/item
            response = await ItemController.AddItemAsync(item, tripId);

            if (response.IsSuccessStatusCode)
            {
                GotDataNotSuccesfull = false;
                return true;
            }
            else
            {
                GotDataNotSuccesfull = true;
                return false;
            }
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO.Create item)
        {

            HttpResponseMessage response;

            //https://localhost:5001/trip/${Trip.tripId}/category
            response = await ItemController.AddCategoryAsync(item, Trip.TripId);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            HttpResponseMessage response;

            //https://localhost:5001/item/{id}
            response = await ItemController.DeleteItemAsync(id);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> MarkItemAsDoneOrNotDone(int itemId, string email)
        {

            HttpResponseMessage response;
            //https://localhost:5001/item/{id}
            response = await ItemController.MarkItemAsDoneOrNotDone(itemId, email);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task UpdateItemAsync(ItemDTO.ForOnePersonOverview sender)
        {

            var response = await MarkItemAsDoneOrNotDone(sender.ItemId, sender.PersonEmail);
            GetTripAsync(Trip.TripId);
        }


        public async Task<bool> ModifyItem(ItemDTO.Overview item)
        {
            Item itemNor = Trip.Items.Find(c => c.ItemId == item.ItemId);
            ItemDTO.Overview itemOver = new ItemDTO.Overview(itemNor);

            HttpResponseMessage response;
            //https://localhost:5001/item
            response = await ItemController.ModifyItem(itemOver);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task InvitePersonToTrip()
        {
            var response = await TripController.InvitePersonToTrip(Trip.TripId, InviteEmail);
            if (response.IsSuccessStatusCode)
            {
                InviteEmail = "";
                GetTripAsync(Trip.TripId);
            }
            else
            {
                GotDataNotSuccesfull = true;
            }
        }

        public async Task CancelInvite(string email)
        {
            var response = await TripController.CancelInvite(Trip.TripId, email);
            if (response.IsSuccessStatusCode)
            {
                GetTripAsync(Trip.TripId);
            }
        }

        public async Task<bool> LeaveTrip()
        {
            var response = await TripController.LeaveTrip(Trip.TripId);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
