using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Worker.DAL;
using Worker.DAL.Models;
using Worker.Models;

namespace Worker.Api.Controllers
{
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

        [HttpGet("{skills}")]
        public async Task<IActionResult> Get(IList<ISkill> skills)
        {
            var workersWithMatchingSkills =  await workerFinder.FindBySkills(skills);
            return Ok(workersWithMatchingSkills);
        }

        [HttpGet("{radius}/{center}")] // TODO: Jak przysłać tu adres? Bo raczej nie Getem.
        public async Task<IActionResult> Get(double radiusInKilometers, IAddress address)
        {
            var workersInVicinity =  await workerFinder.FindInRadiusOfAddress(radiusInKilometers, address);
            return Ok(workersInVicinity);
        }

        [HttpPost]
        public async Task Post([FromBody] IWorkerProfile worker)
        {
            await workerRepository.Save(worker);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] IWorkerProfile worker)
        {
            await workerRepository.Save(id, worker);
        }
    }
}
