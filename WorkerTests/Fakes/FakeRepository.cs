using System.Collections.Generic;
using System.Threading.Tasks;
using Worker;
using Worker.Models;

namespace WorkerTests.Fakes
{
    class FakeRepository : IWorkerRepository
    {
        public Task<IEnumerable<WorkerProfile>> Get(string creator=null)
        {
            IEnumerable<WorkerProfile> workers =  new List<WorkerProfile>
            {
                new WorkerProfile()
                {
                    Name = "Janek Kos",
                    Address = new Address() {City = "Wrocław", Country = "PL", Street = "Kromera", HouseNumber = "1"},
                    Skills = new List<Worker.Models.Skill>
                    {
                        new Skill() { Name = "Swimming"},
                        new Skill() { Name = "Climbing"},
                        new Skill() { Name = "Running"}
                    }
                },
                new WorkerProfile()
                {
                    Name = "Ola Sztos",
                    Address = new Address() {City = "Wrocław", Country = "PL", Street = "Bema", HouseNumber = "2"},
                    Skills = new List<Worker.Models.Skill>
                    {
                        new Skill() { Name = "Swimming"},
                        new Skill() { Name = "Dancing"},
                    }
                },
                new WorkerProfile()
                {
                    Name = "Patryk Gajos",
                    Address = new Address() {City = "Kraków", Country = "PL", Street = "Popiełuszki", HouseNumber = "3"},
                    Skills = new List<Worker.Models.Skill>
                    {
                        new Skill() { Name = "Shooting"},
                        new Skill() { Name = "Dancing"},
                    }
                },
                new WorkerProfile()
                {
                    Name = "Kamil Pracz",
                    Address = new Address() {City = "Wrocław", Country = "PL", Street = "Wyszyńskiego", HouseNumber = "4"},
                    Skills = new List<Worker.Models.Skill>
                    {
                        new Skill() { Name = "Singing"},
                    }
                },
                new WorkerProfile()
                {
                    Name = "Remi Julien",
                    Address = new Address() {City = "Lille", Country = "FR", Street = "Rue Arago", HouseNumber = "5"},
                    Skills = new List<Worker.Models.Skill>
                    {
                        new Skill() { Name = "Swimming"},
                        new Skill() { Name = "Climbing"}
                    }
                },
            };
            return Task.FromResult(workers);
        }

        public Task<WorkerProfile> Get(int id, string creator=null)
        {
            throw new System.NotImplementedException();
        }

        public Task<WorkerProfile> Save(WorkerProfile worker, string creator, int id = 0)
        {
            throw new System.NotImplementedException();
        }
    }
}