using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Worker;
using Worker.DAL;

namespace Worker.DAL
{
    public class WorkerRepository: IWorkerRepository
    {
        private readonly WorkerDbContext dbContext;
        private readonly IMapper mapper;

        public WorkerRepository(WorkerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Models.WorkerProfile>> Get()
        {
            var workers = await dbContext.WorkerProfiles
                .Include(x => x.Address)
                .Include(x => x.Skills)
                .ToListAsync();
            return workers.Select(w => mapper.Map<Models.WorkerProfile>(w));
        }

        public async Task<Models.WorkerProfile> Get(int id)
        {
            // TODO: Co robić z błędami?
            var worker =  await dbContext.WorkerProfiles
                .Include(x => x.Address)
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(x => x.WorkerProfileId == id);
            if (worker == null)
                return null;
            return mapper.Map<Models.WorkerProfile>(worker);
        }

        public Task Save(Models.WorkerProfile worker)
        {
            worker.WorkerProfileId = 0;
            var entity = mapper.Map<Entities.WorkerProfile>(worker);
            dbContext.WorkerProfiles.Add(entity);
            return dbContext.SaveChangesAsync();
        }

        public Task Save(int id, Models.WorkerProfile worker)
        {
            worker.WorkerProfileId = id;
            var entity = mapper.Map<Entities.WorkerProfile>(worker);
            dbContext.WorkerProfiles.Update(entity);
            return dbContext.SaveChangesAsync();
        }
    }
}