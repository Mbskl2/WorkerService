using System.ComponentModel.DataAnnotations;

namespace Worker.DAL.Entities
{
#pragma warning disable 8618
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Street { get; set; }
        [MaxLength(10)]
        public string HouseNumber { get; set; }
        [MaxLength(90)]
        public string City { get; set; }
        [MaxLength(2)]
        public string Country { get; set; }
    }
}