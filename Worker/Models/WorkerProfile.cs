using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Worker.Models
{
#pragma warning disable 8618
    public class WorkerProfile
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public IList<Skill> Skills { get; set; }
    }
}