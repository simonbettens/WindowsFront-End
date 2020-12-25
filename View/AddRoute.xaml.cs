using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using WindowsFront_end.Model;
using WindowsFront_end.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddRoute : Page
    {
        public AddDestinationsViewModel ViewModel { get; set; }
        public AddRoute()
        {
            this.InitializeComponent();
            this.ViewModel = new AddDestinationsViewModel();
            this.DataContext = ViewModel;
            MapService.ServiceToken = "JgmWRYIYzmYgbMh4hvWR~OhL3ZDrSPoQeI - PDC3owow~ArOjM7tKkf3GS9Xr45_idgO58fGVP1IXePZHMNlNtDMIbe5xvEzbE9eUbY1VPr31";
        }

        private async void Map_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            MapLocationFinderResult result = null;
            var tappedGeoPosition = args.Location.Position;
            try
            {


                Geopoint pointToReverseGeocode = new Geopoint(tappedGeoPosition);
                result = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode, MapLocationDesiredAccuracy.High);
            }
            catch (Exception)
            {

                System.Diagnostics.Debug.WriteLine("method end");
            }

            // If the query returns results, display the name of the town
            // contained in the address of the first result.
            if (result != null && result.Status == MapLocationFinderStatus.Success)
            {
                try
                {
                    mapOutput.Text = "town = " +
                      result.Locations[0].Address.Town + " street = " + result.Locations[0].Address.Street + " streetnumber = " + result.Locations[0].Address.StreetNumber;
                    var town = result.Locations[0].Address.Town;
                    var street = result.Locations[0].Address.Street;
                    var streetnumber = result.Locations[0].Address.StreetNumber;
                    var address = String.Format("{0}, {1} {2}", town, street, streetnumber);
                    ViewModel.InputLocation(tappedGeoPosition.Latitude, tappedGeoPosition.Longitude, address);

                }
                catch (Exception)
                {

                    System.Diagnostics.Debug.WriteLine("method end");
                }
            }
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.SaveDestination();
            DrawRouteAsync();
        }

        private async void DrawRouteAsync()
        {
            var size = ViewModel.DestinationsList.Count;
            if (size <= 1) return;
            var destArray = new Destination[size];
            ViewModel.DestinationsList.CopyTo(destArray, 0);
            var path = new List<EnhancedWaypoint>();
            for (var i = 0; i < destArray.Length; i++)
            {
                BasicGeoposition point = new BasicGeoposition() { Latitude = destArray[i].Latitude, Longitude = destArray[i].Longitude };

                if (i == 0 || i == destArray.Length - 1)
                {
                    path.Add(new EnhancedWaypoint(new Geopoint(point), WaypointKind.Stop));

                }
                else
                {
                    path.Add(new EnhancedWaypoint(new Geopoint(point), WaypointKind.Via));

                }
            }

            MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteFromEnhancedWaypointsAsync(path);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                Map.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await Map.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      MapAnimationKind.Linear);
            }
        }
    }
}
