using System.Collections.Generic;

namespace Worker.Models
{
#pragma warning disable 8618
    public class WorkerProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}