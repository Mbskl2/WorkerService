using System;
using System.Collections.Generic;
using System.Text;
using Worker.Models;

namespace Worker.DAL.Models
{
    public class WorkerProfile : IWorkerProfile
    {
        public int WorkerProfileId { get; set; }
        public string Name { get; set; }
        public IAddress Address { get; set; }
        public IList<ISkill> Skills { get; set; }
    }
}
