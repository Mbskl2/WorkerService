using System;
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

        public async Task<IEnumerable<Models.WorkerProfile>> Get(string? creator = null)
        {
            IQueryable<Entities.WorkerProfile> workers = dbContext.WorkerProfiles
                .Include(x => x.Address)
                .Include(x => x.Skills);
            if (Exists(creator))
                workers = workers.Where(w => w.Creator == creator);
            var listOfWorkers = await workers.ToListAsync();
            return listOfWorkers.Select(w => mapper.Map<Models.WorkerProfile>(w));
        }


        public async Task<Models.WorkerProfile?> Get(int id, string? creator = null)
        {
            var worker =  await dbContext.WorkerProfiles
                .Include(x => x.Address)
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (worker == null || BelongsToAnotherCreator(worker, creator))
                return null;
            return mapper.Map<Models.WorkerProfile>(worker);
        }

        public async Task<Models.WorkerProfile> Save(Models.WorkerProfile worker, string creator, int id = 0)
        {
            worker.Id = id;
            var entity = mapper.Map<Entities.WorkerProfile>(worker);
            entity.Creator = creator;
            if (id == 0)
                await dbContext.WorkerProfiles.AddAsync(entity);
            else
                await CopyToExistingEntity(id, entity);
            await dbContext.SaveChangesAsync();
            return mapper.Map<Models.WorkerProfile>(entity);
        }
        private bool Exists(string? creator)
        {
            return !String.IsNullOrWhiteSpace(creator);
        }

        private bool BelongsToAnotherCreator(Entities.WorkerProfile worker, string? creator)
        {
            if (Exists(creator))
                return worker.Creator != creator;
            return false;
        }

        private async Task CopyToExistingEntity(int id, WorkerProfile newWorker)
        {
            Entities.WorkerProfile existingWorker = await GetExistingWorker(id);
            existingWorker.Name = newWorker.Name;
            CopyAddress(existingWorker.Address, newWorker.Address);
            existingWorker.Skills = newWorker.Skills;
        }

        private Task<WorkerProfile> GetExistingWorker(int existingId)
        {
            return dbContext.WorkerProfiles
                .Include(x => x.Skills)
                .FirstAsync(x => x.Id == existingId);
        }

        private void CopyAddress(Address existingAddress, Address newAddress)
        {
            existingAddress.Country = newAddress.Country;
            existingAddress.City = newAddress.City;
            existingAddress.Street = newAddress.Street;
            existingAddress.HouseNumber = newAddress.HouseNumber;
        }
    }
}