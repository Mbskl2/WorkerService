using System.Threading.Tasks;
using Worker.Models;

namespace Worker.Location
{
    public interface IAddressToCoordinatesTranslator
    {
        Task<MapPoint> Translate(IAddress address);
    }
}