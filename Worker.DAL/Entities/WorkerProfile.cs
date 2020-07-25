using System.Collections.Generic;

namespace Worker.DAL.Entities
{
#pragma warning disable 8618
    public class WorkerProfile
    {
        public int WorkerProfileId { get; set; }
        public string Name { get; set; } // TODO: Dodać atrybuty MaxLength i Key
        public Address Address { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}
