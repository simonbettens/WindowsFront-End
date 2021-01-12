using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsBackend.Models.DTO_s;
using WindowsFront_end.Controllers;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;
using WindowsFront_end.Util;

namespace WindowsFront_end.Repository
{
    public static class TripController
    {
        private static readonly HttpClient _client = new HttpClient(new HttpInterceptorHandler());
        private static readonly string _appJson = "application/json";

        public static async Task<List<Trip>> GetAllAsync()
        {
            var list = new List<Trip>();
            var json = await _client.GetStringAsync(new Uri(UrlUtil.ProjectURL + "Trip"));
            var lst = JsonConvert.DeserializeObject<List<TripDTO.Overview>>(json);
            foreach (var tripdto in lst)
            {
                var trip = new Trip(tripdto);
                list.Add(trip);
            }
            return list;
        }

        public static async Task<TripDTO.Detail> GetTripAsync(int id)
        {
            var json = await _client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"trip/{id}"));
            var trip = JsonConvert.DeserializeObject<TripDTO.Detail>(json);

            return trip;
        }

        public static async Task<HttpResponseMessage> CreateTrip(Trip trip)
        {
            TripDTO.Create tripDTO = new TripDTO.Create
            {
                Name = trip.Name,
                Color = trip.Color,
                End = trip.End,
                Start = trip.Start,
                Items = trip.Items.Select(i => new ItemDTO.Overview(i)).ToList(),
                Route = new RouteDTO.Overview(trip.Route)
            };
            var json = JsonConvert.SerializeObject(tripDTO);
            var data = new StringContent(json, Encoding.UTF8, _appJson);
            return await _client.PostAsync(new Uri(UrlUtil.ProjectURL + "trip"), data);
        }

        public static async Task<HttpResponseMessage> AcceptInviteToTrip(int tripId)
        {
            return await _client.PutAsync(new Uri($"trip/{tripId}/accept"), null);
        }

        public static async Task<HttpResponseMessage> DeclineInviteToTrip(int tripId)
        {
            return await _client.DeleteAsync(new Uri($"trip/{tripId}/invite/{ApplicationData.Current.LocalSettings.Values["current_user_email"]}"));
        }
    }


}
