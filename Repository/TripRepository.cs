using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Util;

namespace WindowsFront_end.Repository
{
    public static class TripRepository
    {
        private static readonly HttpClient Client = new HttpClient();
        public static async Task<List<Trip>> GetAllAsync()
        {
            var list = new List<Trip>();
            var json = await Client.GetStringAsync(new Uri(UrlUtil.ProjectURL + "Trip"));
            var lst = JsonConvert.DeserializeObject<List<TripDTO.Overview>>(json);
            foreach (var tripdto in lst)
            {
                var trip = new Trip(tripdto);
                list.Add(trip);
            }
            return list;
        }
    }
}
