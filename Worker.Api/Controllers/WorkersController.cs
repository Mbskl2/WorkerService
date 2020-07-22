using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Worker.DAL.Models;

namespace Worker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WorkerProfile> Get()
        {
            return new WorkerProfile[]
            {
                new WorkerProfile()
                {
                    Address = new Address()
                    {
                        City = "Wrocław", Country = "PL", FlatNumber = "22", HouseNumber = "13a", Id = 1, Street = "Kasprowicza"
                    },
                    Id = 1,
                    Name = "Janek.Kos"
                },
                new WorkerProfile()
                {
                    Address = new Address()
                    {
                        City = "Warszawa", Country = "PL", FlatNumber = "3", HouseNumber = "1", Id = 2, Street = "ul. Adama Mickiewicza"
                    },
                    Id = 2,
                    Name = "Justyna.Szumowska"
                },
            };
        }

        [HttpGet("{id}")]
        public WorkerProfile Get(int id)
        {
            return new WorkerProfile();
        }

        [HttpPost]
        public void Post([FromBody] WorkerProfile value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] WorkerProfile value)
        {
        }
    }
}
