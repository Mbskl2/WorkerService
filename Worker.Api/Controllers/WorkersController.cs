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
        public async Task<IActionResult> Get()
        {
            var allWorkers = await workerRepository.Get();
            return Ok(allWorkers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var worker = await workerRepository.Get(id);
            if (worker == null)
                return NotFound();
            return Ok(worker);
        }

        [HttpGet("bySkills")]
        public async Task<IActionResult> Get([FromQuery] string[] skills)
        {
            var skillList = skills.Select(s => new Skill() {Name = s}).ToList();
            var workersWithMatchingSkills =  await workerFinder.FindBySkills(skillList);
            return Ok(workersWithMatchingSkills);
        }

        [HttpGet("byLocation")]
        public async Task<IActionResult> Get(
            double radiusInKm, string countryIsoCode, string city, string street, string houseNumber)
        {
            var address = new Address()
                {Country = countryIsoCode, City = city, Street = street, HouseNumber = houseNumber};
            var workersInVicinity =  await workerFinder.FindInRadiusOfAddress(radiusInKm, address);
            return Ok(workersInVicinity);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkerProfile worker)
        {
            await workerRepository.Save(worker);
            return Accepted(worker); // TODO: Zwrócić go z wygenerowanym Id
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WorkerProfile worker)
        {
            worker.WorkerProfileId = id;
            if (workerRepository.Get(id) == null)
                return NotFound();
            await workerRepository.Save(id, worker);
            return Accepted(worker);
        }
    }
}
