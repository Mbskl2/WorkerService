using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Worker.DAL.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? FlatNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public WorkerProfile WorkerProfile { get; set; }
    }
}