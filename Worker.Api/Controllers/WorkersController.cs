using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worker.Api.Configuration.AuthZero;
using Worker.Models;

namespace Worker.Api.Controllers
{
    [Authorize]
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

        [Authorize(AuthZeroPermissions.ReadOwnWorkers)]
        [HttpGet]
        public async Task<IActionResult> GetAllWorkers()
        {
            //ControllerContext.HttpContext.User.Identity.Name; // TODO: Jeśli admin readAll:workers to przekazać null. Inaczej creatora
            var allWorkers = await workerRepository.Get();
            return Ok(allWorkers);
        }

        [Authorize(AuthZeroPermissions.ReadOwnWorkers)]
        [HttpGet("{id}", Name = nameof(GetWorkerById))]
        public async Task<IActionResult> GetWorkerById(int id)
        {
            var worker = await workerRepository.Get(id);
            if (worker == null)
                return NotFound();
            return Ok(worker);
        }

        [Authorize(AuthZeroPermissions.SearchWorkers)]
        [HttpGet("bySkills")]
        public async Task<IActionResult> GetWorkersWithMatchingSkills([FromQuery] string[] skills)
        {
            var skillList = skills.Select(s => new Skill() {Name = s}).ToList();
            var workersWithMatchingSkills =  await workerFinder.FindBySkills(skillList);
            return Ok(workersWithMatchingSkills);
        }

        [Authorize(AuthZeroPermissions.SearchWorkers)]
        [HttpGet("byLocation")]
        public async Task<IActionResult> GetWorkersLivingInRadiusOfLocation(
            double radiusInKm, string countryIsoCode, string city, string street, string houseNumber)
        {
            var address = new Address()
                {Country = countryIsoCode, City = city, Street = street, HouseNumber = houseNumber};
            var workersInVicinity =  await workerFinder.FindInRadiusOfAddress(radiusInKm, address);
            return Ok(workersInVicinity);
        }

        [Authorize(AuthZeroPermissions.CreateWorkers)]
        [HttpPost]
        public async Task<IActionResult> CreateWorker([FromBody] WorkerProfile worker)
        {
            var savedWorker = await workerRepository.Save(worker, ""); // TODO: Zmienić ""
            return CreatedAtRoute(nameof(GetWorkerById), new { id = savedWorker.Id }, savedWorker);
        }

        [Authorize(AuthZeroPermissions.ModifyWorkers)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker(int id, [FromBody] WorkerProfile worker)
        {
            worker.Id = id;
            if (workerRepository.Get(id) == null)
                return NotFound();
            var savedWorker = await workerRepository.Save(worker, "", id); // tODO: Zmienić ""
            return CreatedAtRoute(nameof(GetWorkerById), new { id = savedWorker.Id }, savedWorker);
        }
    }
}
