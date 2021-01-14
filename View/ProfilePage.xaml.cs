using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsFront_end.Models;
using WindowsFront_end.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        public ApplicationDataContainer LocalSettings { get; set; }
        public ProfileViewModel ViewModel { get; set; }
        public ProfilePage()
        {
            this.InitializeComponent();
            LocalSettings = ApplicationData.Current.LocalSettings;
            string currentuser = (string)LocalSettings.Values["current_user_email"];
            ViewModel = new ProfileViewModel(currentuser, LocalSettings);
            this.DataContext = ViewModel;
        }

        public async void AcceptInvite(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            await ViewModel.AcceptInvite((int)id);
        }

        public async void DeclineInvite(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            await ViewModel.DeclineInvite((int)id);
        }
    }
}
