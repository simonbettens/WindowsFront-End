using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Util;

namespace WindowsFront_end.Controllers
{
    public static class ItemController
    {
        //a HttpClient with a HttpInterceptorHandler (Authorization : adding the bearer jwt-token)
        private static readonly HttpClient _client = new HttpClient(new HttpInterceptorHandler());
        private static readonly string _appJson = "application/json";
        /// <summary>
        /// flips the item IsDone property in the database
        /// </summary>
        /// <param name="itemId">the itemid we wish to update</param>
        /// <param name="email">the email of the person</param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> MarkItemAsDoneOrNotDone(int itemId, string email)
        {
            var data = new StringContent("", Encoding.UTF8, _appJson);
            try
            {
                return await _client.PutAsync(new Uri(UrlUtil.ProjectURL + $"trip/item/{itemId}/{email}/mark-as-done"), data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// makes a new item in the database for a trip and a person
        /// </summary>
        /// <param name="item">the itemdto we wisht to create and save </param>
        /// <param name="tripId">the tripid where we add the item</param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> AddItemAsync(ItemDTO.Create item, int tripId)
        {
            var Json = JsonConvert.SerializeObject(item);

            var data = new StringContent(Json, Encoding.UTF8, _appJson);
            try
            {
                return await _client.PostAsync(new Uri(UrlUtil.ProjectURL + $"trip/{tripId}/item"), data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// adds and creates a catergory in the database
        /// </summary>
        /// <param name="category">the categorydto we wish to create</param>
        /// <param name="tripId">the tripid where we add the category </param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> AddCategoryAsync(CategoryDTO.Create category, int tripId)
        {
            var Json = JsonConvert.SerializeObject(category);

            var data = new StringContent(Json, Encoding.UTF8, _appJson);
            try
            {
                return await _client.PostAsync(new Uri(UrlUtil.ProjectURL + $"trip/{tripId}/category"), data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// deletes the item in the database
        /// </summary>
        /// <param name="id">the id of the item we wish to delete</param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> DeleteItemAsync(int id)
        {
            try
            {
                return await _client.DeleteAsync(new Uri(UrlUtil.ProjectURL + $"item/{id}"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
