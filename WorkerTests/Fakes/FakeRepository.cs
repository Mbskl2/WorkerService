using System.Collections.Generic;
using System.Threading.Tasks;
using Worker;
using Worker.DAL.Models;
using Worker.Models;

namespace WorkerTests.Fakes
{
    class FakeRepository : IWorkerRepository
    {
        public Task<IEnumerable<IWorkerProfile>> Get()
        {
            IEnumerable<IWorkerProfile> workers =  new List<IWorkerProfile>
            {
                new WorkerProfile()
                {
                    Name = "Janek Kos",
                    Address = new Address() {City = "Wrocław", Country = "PL", Street = "Kromera", HouseNumber = "1"},
                    Skills = new List<ISkill>
                    {
                        new Skill() {Name = "Swimming"},
                        new Skill() {Name = "Climbing"},
                        new Skill() {Name = "Running"}
                    }
                },
                new WorkerProfile()
                {
                    Name = "Ola Sztos",
                    Address = new Address() {City = "Wrocław", Country = "PL", Street = "Bema", HouseNumber = "2"},
                    Skills = new List<ISkill>
                    {
                        new Skill() {Name = "Swimming"},
                        new Skill() {Name = "Dancing"},
                    }
                },
                new WorkerProfile()
                {
                    Name = "Patryk Gajos",
                    Address = new Address() {City = "Kraków", Country = "PL", Street = "Popiełuszki", HouseNumber = "3"},
                    Skills = new List<ISkill>
                    {
                        new Skill() {Name = "Shooting"},
                        new Skill() {Name = "Dancing"},
                    }
                },
                new WorkerProfile()
                {
                    Name = "Kamil Pracz",
                    Address = new Address() {City = "Wrocław", Country = "PL", Street = "Wyszyńskiego", HouseNumber = "4"},
                    Skills = new List<ISkill>
                    {
                        new Skill() {Name = "Singing"},
                    }
                },
                new WorkerProfile()
                {
                    Name = "Remi Julien",
                    Address = new Address() {City = "Lille", Country = "FR", Street = "Rue Arago", HouseNumber = "5"},
                    Skills = new List<ISkill>
                    {
                        new Skill() {Name = "Swimming"},
                        new Skill() {Name = "Climbing"}
                    }
                },
            };
            return Task.FromResult(workers);
        }

        public Task<IWorkerProfile> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save(IWorkerProfile worker)
        {
            throw new System.NotImplementedException();
        }

        public Task Save(int id, IWorkerProfile worker)
        {
            throw new System.NotImplementedException();
        }
    }
}