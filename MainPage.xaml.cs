using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using WindowsFront_end.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsFront_end
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ApplicationDataContainer LocalSettings { get; set; }
        public MainPage()
        {
            LocalSettings = ApplicationData.Current.LocalSettings;
            this.InitializeComponent();

        }
        private void Profile_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(ProfilePage));
        }
        private void Trips_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(TripoverzichtPage));
        }

        private void Members_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(InvitesPage));
        }

        private void Archive_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
        private void Logout_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            LocalSettings.Values["token"] = null;
            this.Frame.Navigate(typeof(LogIn), null, new SuppressNavigationTransitionInfo());
        }
    }
}
