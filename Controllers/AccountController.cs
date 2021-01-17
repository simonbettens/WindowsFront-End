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
        //a normale HttpClient
        private static readonly HttpClient _client = new HttpClient();
        //a HttpClient with a HttpInterceptorHandler (Authorization : adding the bearer jwt-token)
        private static readonly HttpClient _clientWithInterceptor = new HttpClient(new HttpInterceptorHandler());
        private static readonly string _appJson = "application/json";

        /// <summary>
        /// Sends the LoginDTO to the api
        /// </summary>
        /// <param name="login">a dto with all properties that are needed to perform the login</param>
        /// <returns>the response message (including the statuscode and if it has been succesfull the jwt-token)</returns>
        public static async Task<HttpResponseMessage> Login(LoginDTO login)
        {
            var loginJson = JsonConvert.SerializeObject(login);
            var data = new StringContent(loginJson, Encoding.UTF8, _appJson);
            var response = await _client.PostAsync(new Uri(UrlUtil.ProjectURL + "person/login"),
                                data);
            return response;
        }
        /// <summary>
        /// Sends the RegisterDTO to the api
        /// </summary>
        /// <param name="register">a dto with all properties that are needed to perform the registration</param>
        /// <returns>the response message (including the statuscode and if it has been succesfull the jwt-token)</returns>
        public static async Task<HttpResponseMessage> Register(RegisterDTO register)
        {
            var registerJson = JsonConvert.SerializeObject(register);
            var data = new StringContent(registerJson, Encoding.UTF8, _appJson);
            var response = await _client.PostAsync(new Uri(UrlUtil.ProjectURL + "person/register"),
                data);
            return response;
        }
        /// <summary>
        /// gets the person that correlates with the email from the api
        /// </summary>
        /// <param name="email">a email</param>
        /// <returns> a valid person-object or throw an exception (should be handeled)</returns>
        public static async Task<Person> GetPersonByEmail(string email)
        {
            var json = await _client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"person/GetPersonByEmail?email={email}"));
            var account = JsonConvert.DeserializeObject<PersonDTO.FullOverview>(json);
            Person p = new Person(account);
            //await Task.Delay(3000);
            return p;
        }
        /// <summary>
        /// update the person with the given email in the database
        /// </summary>
        /// <param name="email">the email of the to update person</param>
        /// <param name="dto">the dto with the updated properties</param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> UpdatePersonByEmail(string email, PersonDTO.Overview dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, _appJson);
            var response = await _clientWithInterceptor.PutAsync(new Uri(UrlUtil.ProjectURL + $"person?email={email}"), data);
            return response;
        }

    }
}
