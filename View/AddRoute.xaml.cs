﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;
using WindowsFront_end.Models;
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
        public MapElementsLayer CurrentPOIList { get; set; }

        public AddRoute()
        {
            this.InitializeComponent();
            this.ViewModel = new AddDestinationsViewModel();
            this.ViewModel.DestinationsList.CollectionChanged += (sender, e) => AddLine();
            this.ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("SendSuccesfull"))
                {
                    if (ViewModel.SendSuccesfull)
                    {
                        Navigate();
                    }
                }
            };

            this.DataContext = ViewModel;
            MapService.ServiceToken = "JgmWRYIYzmYgbMh4hvWR~OhL3ZDrSPoQeI - PDC3owow~ArOjM7tKkf3GS9Xr45_idgO58fGVP1IXePZHMNlNtDMIbe5xvEzbE9eUbY1VPr31";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Trip trip = (Trip)e.Parameter;
            SetTrip(trip);
        }

        public void SetTrip(Trip trip)
        {
            if (trip != null && trip != ViewModel.Trip)
            {
                ViewModel.Trip = trip;
                ViewModel.DestinationsList.Clear();
                trip.Route.Destinations.ForEach((dest) => ViewModel.DestinationsList.Add(dest));
                AddLine();
                SetCenter();
            }
        }

        private void SetCenter()
        {
            var array = ViewModel.GetDestinationsAsArray();
            if (array.Length > 0)
            {
                var start = array[0];
                BasicGeoposition point = new BasicGeoposition() { Latitude = start.Latitude, Longitude = start.Longitude };
                Geopoint startpoint = new Geopoint(point);
                Map.Center = startpoint;
            }
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
                ViewModel.ErrorMessage = "Er is een fout gebeurd bij het opvragen van deze locatie";
                Debug.WriteLine("method end");
            }
            // If the query returns results, display the name of the town
            // contained in the address of the first result.
            if (result != null && result.Status == MapLocationFinderStatus.Success)
            {
                try
                {
                    var loc = result.Locations[0];
                    ViewModel.InputLocation(tappedGeoPosition.Latitude, tappedGeoPosition.Longitude, loc.Address.FormattedAddress);
                }
                catch (Exception)
                {
                    ViewModel.ErrorMessage = "Er is een fout gebeurd bij het opvragen van deze locatie";
                    Debug.WriteLine("method end");
                }
            }
            else
            {
                ViewModel.ErrorMessage = "Deze locatie kon niet gevonden worden";
            }
        }

        private async void GeocodeButton_Click(object sender, RoutedEventArgs e)
        {
            // The address or business to geocode.
            string addressToGeocode = txbAddress.Text;
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
                ViewModel.InputLocation(loc.Point.Position.Latitude, loc.Point.Position.Longitude, loc.Address.FormattedAddress);
                DrawPoint(loc.Point.Position, "Nu geselecteerd");
            }
            else
            {
                ViewModel.ErrorMessage = "Deze locatie kon niet gevonden worden";
            }
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
        private async void AddLine()
        {
            var size = ViewModel.DestinationsList.Count;
            if (CurrentLine != null)
            {
                Map.Layers.Remove(CurrentLine);
            }
            if (size <= 1) return;
            var destArray = ViewModel.GetDestinationsAsArray();
            var coords = new List<BasicGeoposition>();
            DrawPoints(destArray);
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
            polygon.StrokeDashed = true;

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
            await Map.TrySetViewBoundsAsync(GeoboundingBox.TryCompute(coords), null, MapAnimationKind.None);
            //Map.ZoomLevel = bingMap.ZoomLevel * 0.85

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
                      routeResult.Route.BoundingBox, null, MapAnimationKind.None);
            }
        }

        // <summary>
        /// adds a point on the map
        /// </summary>
        /// <param name="snPosition">the positions where the point needs to be placed</param>
        /// <param name="text">text for context with the marker</param>
        private void DrawPoints(Destination[] destArray)
        {

            var MyLandmarks = new List<MapElement>();
            for (var i = 0; i < destArray.Length; i++)
            {
                var dest = destArray[i];
                BasicGeoposition point = new BasicGeoposition() { Latitude = dest.Latitude, Longitude = dest.Longitude };
                Geopoint snPoint = new Geopoint(point);

                var icon = new MapIcon
                {
                    Location = snPoint,
                    NormalizedAnchorPoint = new Point(0.5, 1.0),
                    ZIndex = 0,
                    Title = dest.Address
                };
                MyLandmarks.Add(icon);
            }
            var LandmarksLayer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = MyLandmarks
            };
            Map.Layers.Remove(CurrentPOIList);
            Map.Layers.Add(LandmarksLayer);
            CurrentPOIList = LandmarksLayer;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTripPage), ViewModel.Trip);
        }

        private void GoOn(object sender, RoutedEventArgs e)
        {
            Navigate();
        }

        private void Navigate()
        {
            Frame.Navigate(typeof(TripDetailsPage), ViewModel.Trip);
        }
    }
}
