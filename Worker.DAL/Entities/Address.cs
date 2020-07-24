namespace Worker.DAL.Entities
{
#pragma warning disable 8618
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}