using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsFront_end.Controllers;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;

namespace WindowsFront_end.ViewModel
{
    public class LoginRegistratieViewModel
    {
        public Person LoggedInPerson { get; set; }
        public bool GotDataNotSuccesfull { get; set; }
        public ApplicationDataContainer LocalSettings { get; }

        public LoginRegistratieViewModel(ApplicationDataContainer localSettings)
        {
            LoggedInPerson = null;
            GotDataNotSuccesfull = false;
            LocalSettings = localSettings;
        }


        public async Task<bool> RegistrationPerson(RegisterDTO p)
        {
            try
            {

                HttpResponseMessage response = await AccountController.Register(p);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    LocalSettings.Values["token"] = token;
                    GotDataNotSuccesfull = false;
                }
                else
                {
                    GotDataNotSuccesfull = true;
                }
                Console.WriteLine(p.ToString() + "     " + response.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }

            return GotDataNotSuccesfull;
        }

        public async Task<bool> LogInPerson(LoginDTO login)
        {
            try
            {
                //test
                //https://localhost:5001/person/login
                HttpResponseMessage response = await AccountController.Login(login);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    LocalSettings.Values["token"] = token;
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
