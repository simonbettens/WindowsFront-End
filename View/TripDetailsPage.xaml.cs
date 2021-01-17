
using System;
using System.Threading;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TripDetailsPage : Page
    {

        public TripDetailViewModel ViewModel { get; set; }
        public int tripId { get; set; }

        private DataTransferManager _dataTransferManager = DataTransferManager.GetForCurrentView();

        public TripDetailsPage()
        {
            this.InitializeComponent();
            ViewModel = new TripDetailViewModel();
            DataContext = ViewModel;


            ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Trip") && ViewModel.Trip != null)
                {
                    AddRouteChildView.SetTrip(ViewModel.Trip);
                }
            };

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Trip trip = e.Parameter as Trip;
            tripId = trip.TripId;
            ViewModel.GetTripAsync(trip.TripId);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += _dataTransferManager_DataRequested;

            MakeVisible();
            // TESTING PURPOSES
            /*var itemLijst = new List<Item>();
            itemLijst.Add(new Item("item 1"));
            itemLijst[0].ItemType = ItemType.ToPack;
            itemLijst.Add(new Item("item 2"));
            itemLijst.Add(new Item("item 3"));
            itemLijst.Add(new Item("item 4"));
            itemLijst.Add(new Item("item 5"));
            itemLijst.Add(new Item("item 6"));
            itemLijst.Add(new Item("item 7"));
            itemLijst.Add(new Item("item 8"));

            Route route = new Route("Route description");

            var destLijst = new List<Destination>();
           destLijst.Add(new Destination("destination 1", "description 1", "Jos Verdegemstraat 21, 9000 gent, belgie"));
           destLijst.Add(new Destination("destination 2", "description 2", "Jos Verdegemstraat 22, 9000 gent, belgie"));
           destLijst.Add(new Destination("destination 3", "description 3", "Jos Verdegemstraat 23, 9000 gent, belgie"));
           destLijst.Add(new Destination("destination 4", "description 4", "Jos Verdegemstraat 24, 9000 gent, belgie"));
           destLijst.Add(new Destination("destination 5", "description 5", "Jos Verdegemstraat 25, 9000 gent, belgie"));
           destLijst.Add(new Destination("destination 6", "description 6", "Jos Verdegemstraat 26, 9000 gent, belgie"));
           destLijst.Add(new Destination("destination 7", "description 7", "Jos Verdegemstraat 27, 9000 gent, belgie"));
           destLijst.Add(new Destination("destination 8", "description 8", "Jos Verdegemstraat 28, 9000 gent, belgie"));

            route.Destinations = destLijst;

            trip.Items = itemLijst;
            trip.Routes.Add(route);
            ViewModel.Trip = trip;*/
        }

        public void MakeVisible()
        {
            blueItem.Visibility = Windows.UI.Xaml.Visibility.Visible;
            titel.Visibility = Windows.UI.Xaml.Visibility.Visible;
            categorieënBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            topack.Visibility = Windows.UI.Xaml.Visibility.Visible;
            toevoegenItem.Visibility = Windows.UI.Xaml.Visibility.Visible;
            blueCat.Visibility = Windows.UI.Xaml.Visibility.Visible;
            categoryName.Visibility = Windows.UI.Xaml.Visibility.Visible;
            toevoegenCategory.Visibility = Windows.UI.Xaml.Visibility.Visible;
            todoTo.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void _dataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "Mijn reis";
            request.Data.SetText(ViewModel.ShareString);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= _dataTransferManager_DataRequested;          
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();

        }

        private async void AddItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ItemType type = ItemType.ToDo;
            if ((bool)todoTo.IsChecked && !(bool)topack.IsChecked)
            {
                type = ItemType.ToDo;
            }
            else if ((bool)topack.IsChecked && !(bool)todoTo.IsChecked)
            {
                type = ItemType.ToPack;
            }
            else
            {
                ViewModel.ErrorMessage = "Je moet één van de twee opties aanduiden!(to do/ to pack)";
                toevoegenJuist.IsOpen = false;
                toevoegenFail.IsOpen = true;
                return;
            }

            string categ = (string)categorieënBox.SelectedItem;
            Category catbasic = ViewModel.Trip.Categories.Find(c => c.Name == categ);
            bool succesful = false;
            try
            {
                if (categ == null || titel == null)
                {
                    ViewModel.ErrorMessage = "Het toevoegen van de item is niet gelukt! Zijn alle parameters ingevuld?";
                    toevoegenJuist.IsOpen = false;
                    toevoegenFail.IsOpen = true;
                }
                else
                {
                    ItemDTO.Create item = new ItemDTO.Create
                    {
                        CategoryId = catbasic.CategoryId,
                        Name = titel.Text,
                        ItemType = (int)type
                    };

                    succesful = await ViewModel.AddItemAsync(item, tripId);
                }
            }
            catch
            {
                ViewModel.ErrorMessage = "Het toevoegen van de item is niet gelukt! Zijn alle parameters ingevuld?";
                toevoegenJuist.IsOpen = false;
                toevoegenFail.IsOpen = true;
            }
            if (succesful)
            {
                ViewModel.GetTripAsync(tripId);
                ViewModel.ErrorMessage = $"{titel.Text} werd succesvol toegevoegd!";
                toevoegenFail.IsOpen = false;
                toevoegenJuist.IsOpen = true;
                titel.Text = "";
            }    
        }

        private async void Invite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.InvitePersonToTrip();
        }

        private async void AddCategory_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)

        {
            if (categoryName.Text.Length == 0)
            {
                ViewModel.ErrorMessage = "Categorie naam moet ingevuld zijn";
                toevoegenJuist.IsOpen = false;
                toevoegenFail.IsOpen = true;
            }
            else
            {
                CategoryDTO.Create category = new CategoryDTO.Create
                {
                    Name = categoryName.Text
                };

                bool succesful = await ViewModel.AddCategoryAsync(category);
                if (succesful)
                {
                    ViewModel.GetTripAsync(tripId);
                    ViewModel.ErrorMessage = $"Categorie {category.Name} werd succesvol toegevoegd aan de Trip!";
                    toevoegenFail.IsOpen = false;
                    toevoegenJuist.IsOpen = true;
                    titel.Text = "";

                }
                else
                {
                    ViewModel.ErrorMessage = "Er is iets misgelopen!";
                    toevoegenJuist.IsOpen = false;
                    toevoegenFail.IsOpen = true;
                }
            }
        }

        private async void DeleteItemToPack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Item item = ViewModel.Trip.Items.Find(c => c.ItemId == (int)((Button)sender).Tag);
            int id = item.ItemId;

            bool succesful = await ViewModel.DeleteItemAsync(id);
            if (succesful)
            {
                ViewModel.GetTripAsync(tripId);
                ViewModel.ErrorMessage = $" {item.Name} werd succesvol verwijderd aan de Trip!";
                toevoegenFail.IsOpen = false;
                toevoegenJuist.IsOpen = true;

            }
            else
            {
                ViewModel.ErrorMessage = "Er is iets misgelopen!";
                toevoegenJuist.IsOpen = false;
                toevoegenFail.IsOpen = true;
            }
        }

        private async void ModifyItemToDoBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ItemDTO.ForOnePersonOverview s = (ItemDTO.ForOnePersonOverview)((Button)sender).Tag;

            Item item = ViewModel.Trip.Items.Find(c => c.ItemId == s.ItemId);
            item.Name = s.Name;
            ItemDTO.Overview itemoverview = new ItemDTO.Overview(item);

            bool succesful = await ViewModel.ModifyItem(itemoverview);
            if (succesful)
            {
                ViewModel.GetTripAsync(tripId);
                ViewModel.ErrorMessage = $" {item.Name} werd succesvol aangepast!";
                toevoegenFail.IsOpen = false;
                toevoegenJuist.IsOpen = true;
            }
            else
            {
                ViewModel.ErrorMessage = "Er is iets misgelopen!";
                toevoegenJuist.IsOpen = false;
                toevoegenFail.IsOpen = true;
            }
        }
        private async void cancel_invite(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string email = ((Button)sender).Tag as string;
            await ViewModel.CancelInvite(email);

        }
    }
}
