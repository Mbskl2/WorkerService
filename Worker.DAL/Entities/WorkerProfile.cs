using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Worker.DAL.Entities
{
#pragma warning disable 8618
    public class WorkerProfile
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public Address Address { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}
