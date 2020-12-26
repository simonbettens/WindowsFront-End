using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using WindowsFront_end.Model.DTO_s;
using WindowsFront_end.View;
using WindowsFront_end.ViewModel;
using Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end
{
    /// <summary>
    /// Pagina waar men inlogt
    /// </summary>
    public sealed partial class LogIn : Page
    {
        public LoginRegistratieViewModel ViewModel { get; set; }
        public LogIn()
        {
            this.InitializeComponent();

            ViewModel = new LoginRegistratieViewModel();
            this.DataContext = ViewModel;
        }

        private void Registreer_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registratie), null, new SuppressNavigationTransitionInfo());
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            bool GotDataNotSuccesfull = true;
            LoginDTO login = new LoginDTO
            {
                Email = email.Text,
                Password = ww.Password
            };
            string token = await ViewModel.LogInPerson(login);
            if(token.Length > 10){
                GotDataNotSuccesfull = false;
            }

            if (GotDataNotSuccesfull)
            {

                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Fout",
                    Content = "Er liep iets mis bij het inloggen. Zijn alle parrameters juist ingevuld?",
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
