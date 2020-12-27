using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using WindowsFront_end.Model;
using WindowsFront_end.Model.DTO_s;
using WindowsFront_end.View;
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
        public Registratie()
        {
            this.InitializeComponent();

            ViewModel = new LoginRegistratieViewModel();
            this.DataContext = ViewModel;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
           
            this.Frame.Navigate(typeof(LogIn),null, new SuppressNavigationTransitionInfo());
        }

        private async void Registreer_Click(object sender, RoutedEventArgs e)
        {

            RegisterDTO person = new RegisterDTO{
                Email = email.Text,
                Password = ww.Password, 
                Name = achternaam.Text, 
                FirstName = voornaam.Text, 
                PasswordConfirmation = wwConfirm.Password, 
                Address = adres.Text 
            };
            bool GotDataNotSuccesfull = await ViewModel.RegistrationPerson(person);

            if (GotDataNotSuccesfull)
            {
                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Fout",
                    Content = "Er liep iets mis bij het registreren. Zijn alle parrameters juist ingevuld?",
                    CloseButtonText = "Ok"
                };

                await noWifiDialog.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(TripoverzichtPage), null, new SuppressNavigationTransitionInfo());
            }
            

        }

    }
}
