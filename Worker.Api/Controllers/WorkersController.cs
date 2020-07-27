using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public WorkersController(
            IWorkerRepository workerRepository,
            WorkerProfileFinder workerFinder,
            IConfiguration configuration)
        {
            this.workerRepository = workerRepository;
            this.workerFinder = workerFinder;
            this.configuration = configuration;
        }

        [Authorize(AuthZeroPermissions.ReadOwnWorkers)]
        [HttpGet]
        public async Task<IActionResult> GetAllWorkers()
        {
            var allWorkers = await workerRepository.Get(GetReader());
            return Ok(allWorkers);
        }

        [Authorize(AuthZeroPermissions.ReadOwnWorkers)]
        [HttpGet("{id}", Name = nameof(GetWorkerById))]
        public async Task<IActionResult> GetWorkerById(int id)
        {
            var worker = await workerRepository.Get(id, GetReader());
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
            var savedWorker = await workerRepository.Save(worker, GetCreator());
            return CreatedAtRoute(nameof(GetWorkerById), new { id = savedWorker.Id }, savedWorker);
        }

        [Authorize(AuthZeroPermissions.ModifyWorkers)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker(int id, [FromBody] WorkerProfile worker)
        {
            worker.Id = id;
            if (workerRepository.Get(id) == null)
                return NotFound();
            var savedWorker = await workerRepository.Save(worker, GetCreator(), id);
            return CreatedAtRoute(nameof(GetWorkerById), new { id = savedWorker.Id }, savedWorker);
        }

        private string GetCreator()
        {
            return ControllerContext.HttpContext.User.Identity.Name ?? "no user";
        }

        private string? GetReader()
        {
            var domain = $"https://{configuration["Auth0:Domain"]}/";
            var canReadAll = ControllerContext.HttpContext.User.Claims.Any(
                c => c.Issuer == domain && c.Value == AuthZeroPermissions.ReadAllWorkers);
            if (canReadAll)
                return null;
            return ControllerContext.HttpContext.User.Identity.Name;
        }
    }
}
