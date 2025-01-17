﻿using System;
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

        public async Task<IEnumerable<WorkerProfile>> FindInRadiusOfAddress(double radiusInKilometers, Address centerAddress)
        {
            MapPoint center = await addressToCoordinatesTranslator.Translate(centerAddress);
            var workers = await workerRepository.Get();
            var tasks 
                = workers.Select(async worker =>
                    new { worker, IsLivingInRadius = await LivesInRadiusOfCenter(radiusInKilometers, worker.Address, center)});
            var workersWithIsLivingInRadius = await Task.WhenAll(tasks);
            return workersWithIsLivingInRadius.Where(x => x.IsLivingInRadius).Select(x => x.worker);
        }

        private async Task<bool> LivesInRadiusOfCenter(double radiusInKilometers, Address address, MapPoint centerCoordinates)
        {
            MapPoint addressCoordinates = await addressToCoordinatesTranslator.Translate(address);
            double distance =  distanceCalculator.CalculateInKm(addressCoordinates, centerCoordinates);
            return distance <= radiusInKilometers;
        }

        public async Task<IEnumerable<WorkerProfile>> FindBySkills(IList<Skill> skills)
        {
            if (skills.Count == 0)
                return new List<WorkerProfile>();
            var allWorkers = await workerRepository.Get();
            return allWorkers.Where(w => HasAllSkills(w, skills));
        }

        private bool HasAllSkills(WorkerProfile worker, IList<Skill> skills)
        {
            return skills.All(s => ContainsSkill(worker.Skills, s));
        }
        private bool ContainsSkill(IList<Skill> skills, Skill skill)
        {
            return skills.Any(s => s.Name.Equals(skill.Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}