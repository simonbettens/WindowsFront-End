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
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _appJson = "application/json";

        public static async Task<HttpResponseMessage> Login(LoginDTO login)
        {
            var loginJson = JsonConvert.SerializeObject(login);
            var data = new StringContent(loginJson, Encoding.UTF8, _appJson);
            var response = await _client.PostAsync(new Uri(UrlUtil.ProjectURL + "person/login"),
                                data);
            return response;
        }
        //https://localhost:5001/person/register
        public static async Task<HttpResponseMessage> Register(RegisterDTO register)
        {
            var registerJson = JsonConvert.SerializeObject(register);
            var data = new StringContent(registerJson, Encoding.UTF8, _appJson);
            var response = await _client.PostAsync(new Uri(UrlUtil.ProjectURL + "person/register"),
                data);
            return response;
        }

        public static async Task<Person> GetPersonByEmail(string email)
        {
            var json = await _client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"person/GetPersonByEmail?email={email}"));
            var account = JsonConvert.DeserializeObject<PersonDTO.OverviewWithItems>(json);
            Person p = new Person(account);
            await Task.Delay(3000);
            return p;
        }

        public static async Task<HttpResponseMessage> UpdatePersonByEmail(string email, PersonDTO.Overview dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, _appJson);
            var response = await _client.PutAsync(new Uri(UrlUtil.ProjectURL + $"person?email={email}"), data);
            return response;
        }

    }
}
