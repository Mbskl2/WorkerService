using System;
using System.Globalization;
using System.Threading.Tasks;
using Worker.Location;
using Worker.Models;

namespace WorkerTests.Fakes
{
    class FakeAddressToCoordinatesTranslator: IAddressToCoordinatesTranslator
    {
        public Task<MapPoint> Translate(IAddress address)
        {
            var latitude = Double.Parse(address.HouseNumber, CultureInfo.InvariantCulture);
            return Task.FromResult(new MapPoint(latitude, 0.0));
        }
    }
}