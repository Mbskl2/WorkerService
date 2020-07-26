using System.ComponentModel.DataAnnotations;
using Worker.Models;

namespace Worker.DAL.Entities
{
#pragma warning disable 8618
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }

    }
}