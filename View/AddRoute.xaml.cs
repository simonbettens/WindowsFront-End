using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
        public MapElementsLayer CurrentPOI { get; set; }
        public MapElementsLayer CurrentLine { get; set; }
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
                DrawPoint(tappedGeoPosition, "Nu geselecteerd");
                Geopoint pointToReverseGeocode = new Geopoint(tappedGeoPosition);
                result = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode, MapLocationDesiredAccuracy.High);
            }
            catch (Exception)
            {

                Debug.WriteLine("method end");
            }

            // If the query returns results, display the name of the town
            // contained in the address of the first result.
            if (result != null && result.Status == MapLocationFinderStatus.Success)
            {
                try
                {
                    var loc = result.Locations[0];
                    mapOutput.Text = loc.Address.FormattedAddress;
                    ViewModel.InputLocation(tappedGeoPosition.Latitude, tappedGeoPosition.Longitude, loc.Address.FormattedAddress);

                }
                catch (Exception)
                {

                    Debug.WriteLine("method end");
                }
            }
        }

        private async void GeocodeButton_Click(object sender, RoutedEventArgs e)
        {
            // The address or business to geocode.
            string addressToGeocode = txbAddress.Text;

            // The nearby location to use as a query hint.
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 47.643;
            queryHint.Longitude = -122.131;
            Geopoint hintPoint = new Geopoint(queryHint);

            // Geocode the specified address, using the specified reference point
            // as a query hint. Return no more than 3 results.
            MapLocationFinderResult result =
                  await MapLocationFinder.FindLocationsAsync(
                                    addressToGeocode,
                                    null,
                                    3);

            // If the query returns results, display the coordinates
            // of the first result.
            if (result.Status == MapLocationFinderStatus.Success)
            {
                var loc = result.Locations[0];
                mapOutput.Text = loc.Address.FormattedAddress;
                ViewModel.InputLocation(loc.Point.Position.Latitude, loc.Point.Position.Longitude, loc.Address.FormattedAddress);

                DrawPoint(loc.Point.Position, "Nu geselecteerd");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveDestination();
            AddLine();
            mapOutput.Text = "Geen locatie geselecteerd";
        }
        /// <summary>
        /// adds a point on the map
        /// </summary>
        /// <param name="snPosition">the positions where the point needs to be placed</param>
        /// <param name="text">text for context with the marker</param>
        private void DrawPoint(BasicGeoposition snPosition, string text)
        {
            var MyLandmarks = new List<MapElement>();

            Geopoint snPoint = new Geopoint(snPosition);

            var spaceNeedleIcon = new MapIcon
            {
                Location = snPoint,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                ZIndex = 0,
                Title = text
            };
            MyLandmarks.Add(spaceNeedleIcon);

            var LandmarksLayer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = MyLandmarks
            };
            Map.Layers.Remove(CurrentPOI);
            Map.Layers.Add(LandmarksLayer);

            Map.Center = snPoint;
            Map.ZoomLevel = 20;

            CurrentPOI = LandmarksLayer;
        }
        /// <summary>
        /// adds a polyline on the map
        /// </summary>
        private void AddLine()
        {
            var size = ViewModel.DestinationsList.Count;
            if (size <= 1) return;
            var destArray = ViewModel.GetDestinationsAsArray();
            var coords = new List<BasicGeoposition>();
            for (var i = 0; i < destArray.Length; i++)
            {
                var dest = destArray[i];
                BasicGeoposition point = new BasicGeoposition() { Latitude = dest.Latitude, Longitude = dest.Longitude };
                coords.Add(point);

            }
            Geopath path = new Geopath(coords);


            MapPolyline polygon = new MapPolyline();
            polygon.StrokeColor = Colors.Blue;
            polygon.StrokeThickness = 5;
            polygon.Path = path;

            var MyLines = new List<MapElement>();
            MyLines.Add(polygon);
            var LinesLayer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = MyLines
            };

            if (CurrentLine != null)
            {
                Map.Layers.Remove(CurrentLine);
            }
            Map.Layers.Add(LinesLayer);
            this.CurrentLine = LinesLayer;


        }
        /// <summary>
        /// method for drawning a route on the map
        /// </summary>
        private async void DrawRouteAsync()
        {
            var size = ViewModel.DestinationsList.Count;
            if (size <= 1) return;
            var destArray = ViewModel.GetDestinationsAsArray();
            var path = new List<EnhancedWaypoint>();
            /*
            BasicGeoposition point1 = new BasicGeoposition() { Latitude = 47.649693, Longitude = -122.144908 };
            BasicGeoposition point2 = new BasicGeoposition() { Latitude = 47.6205, Longitude = -122.3493 };
            BasicGeoposition point3 = new BasicGeoposition() { Latitude = 48.649693, Longitude = -122.144908 };

            path.Add(new EnhancedWaypoint(new Geopoint(point1), WaypointKind.Stop));
            path.Add(new EnhancedWaypoint(new Geopoint(point2), WaypointKind.Via));
            path.Add(new EnhancedWaypoint(new Geopoint(point3), WaypointKind.Stop));
            */

            for (var i = 0; i < destArray.Length; i++)
            {
                var dest = destArray[i];
                BasicGeoposition point = new BasicGeoposition() { Latitude = dest.Latitude, Longitude = dest.Longitude };

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
                      routeResult.Route.BoundingBox, null
                      ,
                      MapAnimationKind.None);
            }
        }
    }
}
