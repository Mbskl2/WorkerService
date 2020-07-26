using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Worker.DAL.Entities;

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
            var worker =  await dbContext.WorkerProfiles
                .Include(x => x.Address)
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (worker == null)
                return null;
            return mapper.Map<Models.WorkerProfile>(worker);
        }
        
        public async Task<Models.WorkerProfile> Save(Models.WorkerProfile worker, int id = 0)
        {
            worker.Id = id;
            var entity = mapper.Map<Entities.WorkerProfile>(worker);
            if (id == 0)
                dbContext.WorkerProfiles.Add(entity);
            else
                await CopyToExistingEntity(id, entity);
            await dbContext.SaveChangesAsync();
            return mapper.Map<Models.WorkerProfile>(entity);
        }

        private async Task CopyToExistingEntity(int id, WorkerProfile newWorker)
        {
            Entities.WorkerProfile existingWorker = await GetExistingWorker(id);
            existingWorker.Name = newWorker.Name;
            existingWorker.Address = mapper.Map<Entities.Address>(newWorker.Address);
            existingWorker.Skills = newWorker.Skills;
        }

        private Task<WorkerProfile> GetExistingWorker(int existingId)
        {
            return dbContext.WorkerProfiles
                .Include(x => x.Skills)
                .FirstAsync(x => x.Id == existingId);
        }
    }
}