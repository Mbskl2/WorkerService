using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worker.Models;

namespace Worker.Api.Controllers
{
    //[Authorize] // TODO: Odkomentować
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerRepository workerRepository;
        private readonly WorkerProfileFinder workerFinder;

        public WorkersController(IWorkerRepository workerRepository, WorkerProfileFinder workerFinder)
        {
            this.workerRepository = workerRepository;
            this.workerFinder = workerFinder;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkers()
        {
            var allWorkers = await workerRepository.Get();
            return Ok(allWorkers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkerById(int id)
        {
            var worker = await workerRepository.Get(id);
            if (worker == null)
                return NotFound();
            return Ok(worker);
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkersWithMatchingSkills([FromQuery] string[] skills)
        {
            var skillList = skills.Select(s => new Skill() {Name = s}).ToList();
            var workersWithMatchingSkills =  await workerFinder.FindBySkills(skillList);
            return Ok(workersWithMatchingSkills);
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkersLivingInRadiusOfLocation(
            double radiusInKm, string countryIsoCode, string city, string street, string houseNumber)
        {
            var address = new Address()
                {Country = countryIsoCode, City = city, Street = street, HouseNumber = houseNumber};
            var workersInVicinity =  await workerFinder.FindInRadiusOfAddress(radiusInKm, address);
            return Ok(workersInVicinity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorker([FromBody] WorkerProfile worker)
        {
            var savedWorker = await workerRepository.Save(worker);
            return CreatedAtRoute(
                nameof(GetWorkerById), new { id = savedWorker.Id }, savedWorker);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker(int id, [FromBody] WorkerProfile worker)
        {
            worker.Id = id;
            if (workerRepository.Get(id) == null)
                return NotFound();
            var savedWorker = await workerRepository.Save(worker, id);
            return CreatedAtRoute(
                nameof(GetWorkerById), new { id = savedWorker.Id }, savedWorker);
        }
    }
}
