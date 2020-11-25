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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Registratie : Page
    {
        public Registratie()
        {
            this.InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
           
            this.Frame.Navigate(typeof(LogIn),null, new SuppressNavigationTransitionInfo());
        }

        private async void Registreer_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person(achternaam.ToString(), voornaam.ToString(), email.ToString(), adres.ToString());
            var personJson = JsonConvert.SerializeObject(person);

            HttpClient client = new HttpClient();
            var res = await client.PostAsync(new Uri("http://localhost:5001/api/Account/registerAdolescent"),
                new HttpStringContent(personJson,Windows.Storage.Streams.UnicodeEncoding.Utf8,"application/json"));

            if(res.IsSuccessStatusCode)
            {

            }

        }

    }
}
