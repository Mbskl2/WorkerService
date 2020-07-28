using System.ComponentModel.DataAnnotations;

namespace Worker.Models
{
#pragma warning disable 8618
    public class Skill
    {
        [Required]
        public string Name { get; set; }
    }
}