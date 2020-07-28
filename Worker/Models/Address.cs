using System.ComponentModel.DataAnnotations;

namespace Worker.Models
{
#pragma warning disable 8618
    public class Address
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}