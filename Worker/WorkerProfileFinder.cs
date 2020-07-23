using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worker.Models;

namespace Worker
{
    public class WorkerProfileFinder
    {
        private readonly IWorkerRepository workerRepository;

        public WorkerProfileFinder(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<IEnumerable<IWorkerProfile>> FindBySkills(IList<ISkill> skills)
        {
            if (skills.Count == 0)
                return new List<IWorkerProfile>();
            var allWorkers = await workerRepository.Get();
            return allWorkers.Where(w => HasAllSkills(w, skills));
        }

        private bool HasAllSkills(IWorkerProfile worker, IList<ISkill> skills)
        {
            return skills.All(s => ContainsSkill(worker.Skills, s));
        }
        private bool ContainsSkill(IList<ISkill> skills, ISkill skill)
        {
            return skills.Any(s => s.Name.Equals(skill.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        public Task<IEnumerable<IWorkerProfile>> FindInRadiusOfAddress(double radiusInKilometers, IAddress center)
        {
            return null;
        }
    }
}