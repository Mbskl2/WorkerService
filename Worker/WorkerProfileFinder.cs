using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worker.Location;
using Worker.Models;

namespace Worker
{
    public class WorkerProfileFinder
    {
        private readonly IWorkerRepository workerRepository;
        private readonly IAddressToCoordinatesTranslator addressToCoordinatesTranslator;
        private readonly DistanceCalculator distanceCalculator;

        public WorkerProfileFinder(
            IWorkerRepository workerRepository,
            IAddressToCoordinatesTranslator addressToCoordinatesTranslator,
            DistanceCalculator distanceCalculator)
        {
            this.workerRepository = workerRepository;
            this.addressToCoordinatesTranslator = addressToCoordinatesTranslator;
            this.distanceCalculator = distanceCalculator;
        }

        public async Task<IEnumerable<IWorkerProfile>> FindInRadiusOfAddress(double radiusInKilometers, IAddress centerAddress)
        {
            MapPoint center = await addressToCoordinatesTranslator.Translate(centerAddress);
            var workers = await workerRepository.Get();
            var tasks 
                = workers.Select(async worker =>
                    new { worker, IsLivingInRadius = await LivesInRadiusOfCenter(radiusInKilometers, worker.Address, center)});
            var workersWithIsLivingInRadius = await Task.WhenAll(tasks);
            return workersWithIsLivingInRadius.Where(x => x.IsLivingInRadius).Select(x => x.worker);
        }

        private async Task<bool> LivesInRadiusOfCenter(double radiusInKilometers, IAddress address, MapPoint centerCoordinates)
        {
            MapPoint addressCoordinates = await addressToCoordinatesTranslator.Translate(address);
            double distance =  distanceCalculator.CalculateInKm(addressCoordinates, centerCoordinates);
            return distance <= radiusInKilometers;
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
    }
}