namespace Worker.Models
{
    public interface IAddress
    {
        int AddressId { get; set; }
        string Street { get; set; }
        string HouseNumber { get; set; }
        string? FlatNumber { get; set; }
        string City { get; set; }
        string Country { get; set; }
    }
}