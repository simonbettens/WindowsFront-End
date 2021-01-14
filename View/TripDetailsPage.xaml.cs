
using System;
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

        private async void toevoegenItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
                ContentDialog itemfoutDialog = new ContentDialog()
                {
                    Title = "Fout",
                    Content = "Je moet één van de twee opties aanduiden! (to do/to pack)",
                    CloseButtonText = "Ok"
                };

                await itemfoutDialog.ShowAsync();
                return;
            }

            string categ = (string)categorieënBox.SelectedItem;
            Category catbasic = ViewModel.Trip.Categories.Find(c => c.Name == categ);
            bool succesful = false;
            ItemDTO.Create item = null;
            try
            {
                item = new ItemDTO.Create
                {
                    CategoryId = catbasic.CategoryId,
                    Name = titel.Text,
                    ItemType = (int)type
                };
                succesful = await ViewModel.AddItemAsync(item, tripId);


            }
            catch
            {
                succesful = false;
            }
            if (succesful)
            {
                ContentDialog categoryJustDialog2 = new ContentDialog()
                {
                    Title = "Succes",
                    Content = $"{item.Name} werd succesvol toegevoegd aan de Trip!",
                    CloseButtonText = "Ok"
                };

                await categoryJustDialog2.ShowAsync();
                ViewModel.GetTripAsync(tripId);
                categoryName.Text = "";
            }
            else
            {
                ContentDialog itemfoutDialog3 = new ContentDialog()
                {
                    Title = "Fout",
                    Content = "Er liep iets mis!",
                    CloseButtonText = "Ok"
                };

                await itemfoutDialog3.ShowAsync();

            }
        }

        private async void toevoegenCategory_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (categoryName.Text.Length == 0)
            {
                ContentDialog categoryfoutDialog = new ContentDialog()
                {
                    Title = "Fout",
                    Content = "Categorie naam moet ingevuld zijn",
                    CloseButtonText = "Ok"
                };

                await categoryfoutDialog.ShowAsync();
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
                    ContentDialog categoryJustDialog = new ContentDialog()
                    {
                        Title = "Succes",
                        Content = $"Categorie {category.Name} werd succesvol toegevoegd aan de Trip!",
                        CloseButtonText = "Ok"
                    };

                    await categoryJustDialog.ShowAsync();
                    ViewModel.GetTripAsync(tripId);

                    titel.Text = "";

                }
                else
                {
                    ContentDialog categoryfoutDialog2 = new ContentDialog()
                    {
                        Title = "Fout",
                        Content = "Er is iets misgelopen!",
                        CloseButtonText = "Ok"
                    };

                    await categoryfoutDialog2.ShowAsync();
                }
            }
        }

        private async void DeleteItemToPack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Item item = ViewModel.Trip.Items.Find(c => c.Name == ((Button)sender).Tag.ToString());
            int id = item.ItemId;

            bool succesful = await ViewModel.DeleteItemAsync(id);
            if (succesful)
            {
                ContentDialog categoryJustDialog3 = new ContentDialog()
                {
                    Title = "Succes",
                    Content = $" {((Button)sender).Tag} werd succesvol verwijderd aan de Trip!",
                    CloseButtonText = "Ok"
                };

                await categoryJustDialog3.ShowAsync();
                ViewModel.GetTripAsync(tripId);

                titel.Text = "";

            }
            else
            {
                ContentDialog categoryfoutDialog3 = new ContentDialog()
                {
                    Title = "Fout",
                    Content = "Er is iets misgelopen!",
                    CloseButtonText = "Ok"
                };

                await categoryfoutDialog3.ShowAsync();
            }
        }
    }
}
