using System.Collections.Generic;
using Worker.DAL.Models;

namespace Worker
{
    public class WorkerProfileFinder
    {
        public IEnumerable<WorkerProfile> FindBySkills(IList<Skill> skills)
        {
            return null;
        }

        public IEnumerable<WorkerProfile> FindInRadiusOfAddress(double radiusInKilometers, Address center)
        {
            return null;
        }
    }
}