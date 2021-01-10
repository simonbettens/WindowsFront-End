using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Util;

namespace WindowsFront_end.Controllers
{
    public static class AccountController
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<Person> GetPersonByEmail(string email)
        {

            var json = await Client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"person/GetPersonByEmail?email={email}"));
            var account = JsonConvert.DeserializeObject<PersonDTO.OverviewWithItems>(json);
            Person p = new Person(account);
            await Task.Delay(3000);
            return p;
        }

        public static async Task<HttpResponseMessage> UpdatePersonByEmail(string email, PersonDTO.Overview dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(new Uri(UrlUtil.ProjectURL + $"person?email={email}"), data);
            return response;
        }

    }
}
