using System.Collections.Generic;

namespace Worker.Models
{
    public interface IWorkerProfile
    {
        int WorkerProfileId { get; set; }
        string Name { get; set; }
        IAddress Address { get; set; }
        IList<ISkill> Skills { get; set; }
    }
}