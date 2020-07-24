using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Location.ResponseModels;
using Microsoft.Extensions.Configuration;
using Worker.Location;

namespace Location
{
    public class GeocodingService
    {
        private readonly string apiKey;
        public HttpClient Client { get; }

        public GeocodingService(HttpClient client, string apiKey)
        {
            this.apiKey = apiKey;

            client.BaseAddress = new Uri("https://maps.googleapis.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
        }

        internal async Task<MapPoint> GetCoordinates(string addressString)
        {
            var response = await CallGeocodingApi(addressString);
            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var responseModel = await Deserialize(responseStream);
                var location = responseModel.Results.First().Geometry.Location;
                return new MapPoint(location.Lat, location.Lng);
            }
        }

        private Task<HttpResponseMessage> CallGeocodingApi(string addressString)
        {
            string requestUri = $@"/maps/api/geocode/json?address={addressString}&key={apiKey}";
            return Client.GetAsync(requestUri);
        }

        private Task<Response> Deserialize(Stream responseStream)
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.DeserializeAsync<Response>(responseStream, options).AsTask();
        }
    }
}