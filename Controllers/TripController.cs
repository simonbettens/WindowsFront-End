using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        //a HttpClient with a HttpInterceptorHandler (Authorization : adding the bearer jwt-token)
        private static readonly HttpClient _client = new HttpClient(new HttpInterceptorHandler());
        private static readonly string _appJson = "application/json";
        /// <summary>
        /// gets all the trips for the user who is logged in
        /// </summary>
        /// <returns>a list of all the trips or an exception (should be handeled)</returns>
        public static async Task<List<Trip>> GetAllAsync()
        {
            var list = new List<Trip>();
            var json = await _client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"trip/email/{ApplicationData.Current.LocalSettings.Values["current_user_email"]}"));
            var lst = JsonConvert.DeserializeObject<List<TripDTO.Overview>>(json);
            foreach (var tripdto in lst)
            {
                var trip = new Trip(tripdto);
                list.Add(trip);
            }
            return list;
        }
        /// <summary>
        /// gets all the trips but in the form of the dto (less unnecesary data over the wire)
        /// </summary>
        /// <returns>list of tripdto type overview</returns>
        public static async Task<List<TripDTO.Overview>> GetAllSimpleAsync()
        {
            var list = new List<Trip>();
            var json = await _client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"trip/email/{ApplicationData.Current.LocalSettings.Values["current_user_email"]}"));
            var lst = JsonConvert.DeserializeObject<List<TripDTO.Overview>>(json);
            return lst;
        }
        /// <summary>
        /// gets the request trip by id
        /// </summary>
        /// <param name="id">the tripid </param>
        /// <returns>the tripdto we wish to get</returns>
        public static async Task<TripDTO.Detail> GetTripAsync(int id)
        {
            var json = await _client.GetStringAsync(new Uri(UrlUtil.ProjectURL + $"trip/{id}"));
            var trip = JsonConvert.DeserializeObject<TripDTO.Detail>(json);

            return trip;
        }
        /// <summary>
        /// create a trip
        /// </summary>
        /// <param name="trip">a trip that needs to be mapped</param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> CreateTrip(Trip trip)
        {
            TripDTO.Create tripDTO = new TripDTO.Create
            {
                Name = trip.Name,
                Color = trip.Color,
                End = trip.End,
                Start = trip.Start,
                Items = new List<ItemDTO.Overview>(),
                Route = new RouteDTO.Overview(trip.Route),
                Travelers = trip.Travelers.Select(i => new PersonDTO.Overview(i)).ToList()
            };
            var json = JsonConvert.SerializeObject(tripDTO);
            var data = new StringContent(json, Encoding.UTF8, _appJson);
            return await _client.PostAsync(new Uri(UrlUtil.ProjectURL + "trip"), data);
        }
        /// <summary>
        /// accept a invite to a trip
        /// </summary>
        /// <param name="tripId">the tripid we wish to join</param>
        /// <returns>he response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> AcceptInviteToTrip(int tripId)
        {
            return await _client.PutAsync(new Uri($"{UrlUtil.ProjectURL}trip/{tripId}/accept"), null);
        }
        /// <summary>
        /// decline a invite to a trip
        /// </summary>
        /// <param name="tripId">the tripid we don't wish to join</param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> DeclineInviteToTrip(int tripId)
        {
            return await _client.DeleteAsync(new Uri($"{UrlUtil.ProjectURL}trip/{tripId}/invite/{ApplicationData.Current.LocalSettings.Values["current_user_email"]}"));
        }
        /// <summary>
        /// invite a person to a trip
        /// </summary>
        /// <param name="tripId">the tripid of the trip</param>
        /// <param name="email">the email of the person</param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> InvitePersonToTrip(int tripId, string email)
        {
            return await _client.PostAsync(new Uri($"{UrlUtil.ProjectURL}trip/{tripId}/invite/{email}"), null);
        }
        /// <summary>
        /// cancels a invite of a person to a trip
        /// </summary>
        /// <param name="tripId">the tripid of the trip</param>
        /// <param name="email">the email of the person</param>
        /// <returns>the response message (including the statuscode)</returns>
        public static async Task<HttpResponseMessage> CancelInvite(int tripId, string email)
        {
            return await _client.DeleteAsync(new Uri($"{UrlUtil.ProjectURL}trip/{tripId}/invite/{email}"));
        }

        public static async Task<HttpResponseMessage> LeaveTrip(int tripId)
        {
            return await _client.DeleteAsync(new Uri($"{UrlUtil.ProjectURL}trip/{tripId}/travelers/remove/{ApplicationData.Current.LocalSettings.Values["current_user_email"]}"));
        }
    }


}
