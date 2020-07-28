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

        /// <summary>
        /// Returns all visible worker profiles in the db.
        /// Requires readOwn:workers permission.
        /// Users which also have readAll:workers will see all the profiles.
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthZeroPermissions.ReadOwnWorkers)]
        [HttpGet]
        public async Task<IActionResult> GetAllWorkers()
        {
            var allWorkers = await workerRepository.Get(GetReader());
            return Ok(allWorkers);
        }

        /// <summary>
        /// Returns the worker profile with the given id if it exists and is
        /// visible to the user. In other cases return 404 Not Found.
        /// Requires readOwn:workers permission to see own worker profiles
        /// and readAll:workers see all of them.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthZeroPermissions.ReadOwnWorkers)]
        [HttpGet("{id}", Name = nameof(GetWorkerById))]
        public async Task<IActionResult> GetWorkerById(int id)
        {
            var worker = await workerRepository.Get(id, GetReader());
            if (worker == null)
                return NotFound();
            return Ok(worker);
        }

        /// <summary>
        /// Returns all worker profiles which have all the specified skills.
        /// If the list is empty, return an empty list.
        /// Requires search:workers permission.
        /// </summary>
        /// <param name="skills"></param>
        /// <returns></returns>
        [Authorize(AuthZeroPermissions.SearchWorkers)]
        [HttpGet("bySkills")]
        public async Task<IActionResult> GetWorkersWithMatchingSkills([FromQuery] string[] skills)
        {
            var skillList = skills.Select(s => new Skill() {Name = s}).ToList();
            var workersWithMatchingSkills =  await workerFinder.FindBySkills(skillList);
            return Ok(workersWithMatchingSkills);
        }

        /// <summary>
        /// Returns all worker profiles where the distance between their
        /// addresses and the specified one is less or equal to the radius.
        /// Requires search:workers permission.
        /// </summary>
        /// <param name="radiusInKm"></param>
        /// <param name="countryIsoCode"></param>
        /// <param name="city"></param>
        /// <param name="street"></param>
        /// <param name="houseNumber"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates the specified worker profile.
        /// Requires create:workers permission.
        /// </summary>
        /// <param name="worker"></param>
        /// <returns></returns>
        [Authorize(AuthZeroPermissions.CreateWorkers)]
        [HttpPost]
        public async Task<IActionResult> CreateWorker([FromBody] WorkerProfile worker)
        {
            var savedWorker = await workerRepository.Save(worker, GetCreator());
            return CreatedAtRoute(nameof(GetWorkerById), new { id = savedWorker.Id }, savedWorker);
        }

        /// <summary>
        /// Modifies the existing worker with the given id. If it doesn't exist
        /// then 404 Nor Found is returned.
        /// Requires modify:workers permission.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="worker"></param>
        /// <returns></returns>
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
