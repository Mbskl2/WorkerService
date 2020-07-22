using System;
using System.Collections.Generic;
using System.Text;

namespace Worker.DAL.Models
{
    public class WorkerProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
