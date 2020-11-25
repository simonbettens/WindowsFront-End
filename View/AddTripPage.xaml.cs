using Windows.UI;
using Windows.UI.Xaml.Controls;
using WindowsFront_end.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddTripPage : Page
    {
        public AddTripViewModel AddTripViewModel { get; set; }

        public AddTripPage()
        {
            this.InitializeComponent();
            AddTripViewModel = new AddTripViewModel();
            this.DataContext = AddTripViewModel;
        }
        private void TripColor_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            Color color = TripColor.Color;
            string hexString = string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
            AddTripViewModel.Trip.Color = hexString;
            AddTripViewModel.CheckAreFieldValid();

        }

        private void TripStart_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            AddTripViewModel.Trip.Start = TripStart.Date.DateTime;
            AddTripViewModel.CheckAreFieldValid();
        }

        private void TripEnd_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            AddTripViewModel.Trip.End = TripEnd.Date.DateTime;
            AddTripViewModel.CheckAreFieldValid();
        }
        /// <summary>
        /// Cancel en navigate back
        /// </summary>
        private void Cancel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TripoverzichtPage));
        }

        private void Save_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AddTripViewModel.Save();
        }

    }

}
