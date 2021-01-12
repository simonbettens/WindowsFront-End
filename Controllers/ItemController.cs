using System;
using System.Net.Http;
using System.Threading.Tasks;
using WindowsFront_end.Util;

namespace WindowsFront_end.Controllers
{
    public static class ItemController
    {
        private static readonly HttpClient _client = new HttpClient(new HttpInterceptorHandler());
        private static readonly string _appJson = "application/json";

        public static async Task<HttpResponseMessage> MarkItemAsDoneOrNotDone(int itemId, string email)
        {
            var data = new StringContent("", Encoding.UTF8, "application/json");
            return await _client.PutAsync(new Uri(UrlUtil.ProjectURL + $"trip/item/{itemId}/{email}/mark-as-done"), data);
        }
    }
}
