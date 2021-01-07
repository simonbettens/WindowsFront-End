using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end
{
    /// <summary>
    /// Pagina waar men zich registreert
    /// </summary>
    public sealed partial class Registratie : Page
    {
        public LoginRegistratieViewModel ViewModel { get; set; }
        public ApplicationDataContainer LocalSettings { get; set; }

        public Registratie()
        {
            this.InitializeComponent();
            LocalSettings = ApplicationData.Current.LocalSettings;
            ViewModel = new LoginRegistratieViewModel(LocalSettings);
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

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(LogIn), null, new SuppressNavigationTransitionInfo());
        }

        private async void Registreer_Click(object sender, RoutedEventArgs e)
        {

            RegisterDTO person = new RegisterDTO
            {
                Email = email.Text,
                Password = ww.Password,
                Name = achternaam.Text,
                FirstName = voornaam.Text,
                PasswordConfirmation = wwConfirm.Password,
                Address = adres.Text
            };
            LocalSettings.Values["current_user_email"] = email.Text;
            bool GotDataNotSuccesfull = await ViewModel.RegistrationPerson(person);

            if (GotDataNotSuccesfull)
            {
                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Fout",
                    Content = "Er liep iets mis bij het registreren. Zijn alle parameters juist ingevuld?",
                    CloseButtonText = "Ok"
                };

                await noWifiDialog.ShowAsync();
            }
            else
            {
                Navigate();
                //this.Frame.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
            }


        }
        private void Navigate()
        {
            this.Frame.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CheckIfLoggedIn();
        }
    }
}
