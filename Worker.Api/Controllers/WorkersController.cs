using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Worker.DAL;
using Worker.DAL.Models;

namespace Worker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly WorkerDbContext dbContext;
        private readonly WorkerProfileFinder workerFinder;

        public WorkersController(WorkerDbContext dbContext, WorkerProfileFinder workerFinder)
        {
            this.dbContext = dbContext;
            this.workerFinder = workerFinder;
        }

        [HttpGet]
        public IEnumerable<WorkerProfile> Get()
        {
            return dbContext.WorkerProfiles 
                .Include(x => x.Address) // TODO: Może include można zapisać ładniej w tej drugiej notacji?
                .Include(x => x.Skills);
        }

        [HttpGet("{id}")]
        public WorkerProfile Get(int id)
        {
            // TODO: Czy powinienem to zrobić async?
            // TODO: Co robić z błędami?
            return dbContext.WorkerProfiles
                .Include(x => x.Address)
                .Include(x => x.Skills)
                .First(x => x.WorkerProfileId == id);
        }

        [HttpGet("{skills}")]
        public IEnumerable<WorkerProfile> Get(IList<Skill> skills)
        {
            return workerFinder.FindBySkills(skills);
        }

        [HttpGet("{skills}")] // TODO: Jak przysłać tu adres? Bo raczej nie Getem.
        public IEnumerable<WorkerProfile> Get(double radiusInKilometers, Address address)
        {
            return workerFinder.FindInRadiusOfAddress(radiusInKilometers, address);
        }

        [HttpPost]
        public void Post([FromBody] WorkerProfile worker)
        {
            worker.WorkerProfileId = 0;
            dbContext.WorkerProfiles.Add(worker);
            dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] WorkerProfile worker)
        {
            worker.WorkerProfileId = id;
            dbContext.WorkerProfiles.Update(worker);
            dbContext.SaveChanges();
        }
    }
}
