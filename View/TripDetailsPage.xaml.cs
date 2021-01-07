using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WindowsFront_end.Models;
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

        public TripDetailsPage()
        {
            this.InitializeComponent();
            ViewModel = new TripDetailViewModel();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            

            Trip trip = e.Parameter as Trip;
            ViewModel.GetTripAsync(trip.TripId);
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
    }
}
