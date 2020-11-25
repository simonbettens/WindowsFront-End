using Windows.UI.Xaml.Controls;
using WindowsFront_end.Model;
using WindowsFront_end.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TripoverzichtPage : Page
    {
        public TripOverzichtViewModel ViewModel { get; set; }

        public TripoverzichtPage()
        {
            this.InitializeComponent();
            ViewModel = new TripOverzichtViewModel();
            this.DataContext = ViewModel;
        }

        private void AddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTripPage));
        }

        private void TripsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Trip selectedToDoList = (Trip)TripsGrid.SelectedItem;

        }
    }
}
