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

        public async Task Save(int id, Models.WorkerProfile worker)
        {
            worker.WorkerProfileId = id;
            var entity = mapper.Map<Entities.WorkerProfile>(worker);
            await CopyToExistingEntity(id, entity);
            await dbContext.SaveChangesAsync();
        }

        private async Task CopyToExistingEntity(int id, WorkerProfile newWorker)
        {
            Entities.WorkerProfile existingWorker = await GetExistingWorker(id);
            existingWorker.Name = newWorker.Name;
            CopyAddressData(newWorker.Address, existingWorker.Address);
            existingWorker.Skills = newWorker.Skills;
        }

        private Task<WorkerProfile> GetExistingWorker(int existingId)
        {
            return dbContext.WorkerProfiles
                .Include(x => x.Skills)
                .FirstAsync(x => x.WorkerProfileId == existingId);
        }

        private void CopyAddressData(Entities.Address src, Entities.Address dst)
        {
            dst.Country = src.Country;
            dst.City = src.City;
            dst.Street = src.Street;
            dst.HouseNumber = src.HouseNumber;
        }


        private void RemoveRepeatedSkills(Entities.WorkerProfile newWorker, Entities.WorkerProfile existingWorker)
        {
            var newSkills = newWorker.Skills.Except(existingWorker.Skills, new SkillEqualityComparer());
            newSkills.ToList().ForEach(s => existingWorker.Skills.Add(s));
        }
    }
}