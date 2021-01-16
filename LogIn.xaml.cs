using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.View;
using WindowsFront_end.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end
{
    /// <summary>
    /// Pagina waar men inlogt
    /// </summary>
    public sealed partial class LogIn : Page
    {
        public LoginRegistratieViewModel ViewModel { get; set; }
        public ApplicationDataContainer LocalSettings { get; set; }
        public LogIn()
        {
            this.InitializeComponent();
            LocalSettings = ApplicationData.Current.LocalSettings;
            ViewModel = new LoginRegistratieViewModel(LocalSettings);
            ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("GotDataNotSuccesfull") && !ViewModel.GotDataNotSuccesfull)
                {
                    Navigate();
                }
            };
            this.DataContext = ViewModel;
        }

        private void CheckIfLoggedIn()
        {
            string token = (string)LocalSettings.Values["token"];
            if (token != null)
            {
                Navigate();
            }
        }

        private void Registreer_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registratie), null, new SuppressNavigationTransitionInfo());
        }

        private void Navigate()
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CheckIfLoggedIn();
        }
    }
}
