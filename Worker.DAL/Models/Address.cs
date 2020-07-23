using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Worker.Models;

namespace Worker.DAL.Models
{
    public class Address : IAddress
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? FlatNumber { get; set; } // TODO: To można wyrzucić
        public string City { get; set; }
        public string Country { get; set; }
    }
}