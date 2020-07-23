using System.Collections.Generic;
using System.Threading.Tasks;
using Worker.Models;

namespace Worker
{
    public class WorkerProfileFinder
    {
        public Task<IEnumerable<IWorkerProfile>> FindBySkills(IList<ISkill> skills)
        {
            return null;
        }

        public Task<IEnumerable<IWorkerProfile>> FindInRadiusOfAddress(double radiusInKilometers, IAddress center)
        {
            return null;
        }
    }
}