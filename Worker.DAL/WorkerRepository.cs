using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worker;
using Worker.DAL.Models;
using Worker.Models;

namespace Worker.DAL
{
    public class WorkerRepository: IWorkerRepository
    {
        readonly WorkerDbContext dbContext;

        public WorkerRepository(WorkerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IWorkerProfile>> Get()
        {
            return await dbContext.WorkerProfiles
                .Include(x => x.Address)
                .Include(x => x.Skills)
                .ToListAsync();
        }

        public async Task<IWorkerProfile> Get(int id)
        {
            // TODO: Co robić z błędami?
            return await dbContext.WorkerProfiles
                .Include(x => x.Address)
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(x => x.WorkerProfileId == id);
        }

        public Task Save(IWorkerProfile worker)
        {
            worker.WorkerProfileId = 0;
            dbContext.WorkerProfiles.Add((WorkerProfile)worker);
            return dbContext.SaveChangesAsync();
        }

        public Task Save(int id, IWorkerProfile worker)
        {
            worker.WorkerProfileId = id;
            dbContext.WorkerProfiles.Update((WorkerProfile)worker);
            return dbContext.SaveChangesAsync();
        }
    }
}