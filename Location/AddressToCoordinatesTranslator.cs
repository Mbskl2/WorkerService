using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Worker;
using Worker.Location;
using Worker.Models;

namespace Location
{
    public class AddressToCoordinatesTranslator : IAddressToCoordinatesTranslator
    {
        private readonly GeocodingService geocodingApi;

        public AddressToCoordinatesTranslator(GeocodingService geocodingService)
        {
            geocodingApi = geocodingService;
        }

        public async Task<MapPoint> Translate(Address address)
        {
            var addressString = CreateAddressString(address);
            return await geocodingApi.GetCoordinates(addressString);
        }

        private string CreateAddressString(Address a)
        {
            var country = new RegionInfo(a.Country.Trim());
            return $@"{a.HouseNumber} {a.Street} {a.City}, {country.EnglishName}";
        }
    }
}