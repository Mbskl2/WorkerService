using System;
using System.Collections.Generic;
using System.Text;

namespace Worker.DAL.Models
{
    public class WorkerProfile
    {
        public int WorkerProfileId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}
