using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Util;

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
            var registerJson = JsonConvert.SerializeObject(p);
            Console.WriteLine(registerJson.ToString());
            HttpResponseMessage response;
            HttpClient client = new HttpClient();
            try
            {
                var data = new StringContent(registerJson, Encoding.UTF8, "application/json");
                //test
                //https://localhost:5001/person/register
                //https://localhost:5001/person/register
                response = await client.PostAsync(new Uri(UrlUtil.ProjectURL + "person/register"),
                    data);
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
                Console.WriteLine(registerJson.ToString() + "     " + response.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }

            return GotDataNotSuccesfull;
        }

        public async Task<bool> LogInPerson(LoginDTO login)
        {
            var loginJson = JsonConvert.SerializeObject(login);

            HttpClient client = new HttpClient();
            var data = new StringContent(loginJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                //test
                //https://localhost:5001/person/login
                response = await client.PostAsync(new Uri(UrlUtil.ProjectURL + "person/login"),
                   data);

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
