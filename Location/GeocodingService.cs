using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Location.ResponseModels;
using Worker.Location;

namespace Location
{
    public class GeocodingService
    {
        private readonly IApiKey apiKey;
        public HttpClient Client { get; }

        public GeocodingService(HttpClient client, IApiKey apiKey)
        {
            this.apiKey = apiKey;

            client.BaseAddress = new Uri("https://maps.googleapis.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
        }

        internal async Task<MapPoint> GetCoordinates(string addressString)
        {
            Response response = await GetResponse(addressString);
            var location = response.Results.First().Geometry.Location;
            return new MapPoint(location.Lat, location.Lng);
        }

        private Task<HttpResponseMessage> CallGeocodingApi(string addressString)
        {
            string requestUri = $@"/maps/api/geocode/json?address={addressString}&key={apiKey.Get()}";
            return Client.GetAsync(requestUri);
        }
        private async Task<Response> GetResponse(string addressString)
        {
            var response = await CallGeocodingApi(addressString);
            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await Deserialize(responseStream);
        }

        private Task<Response> Deserialize(Stream responseStream)
        {
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.DeserializeAsync<Response>(responseStream, options).AsTask();
        }
    }
}