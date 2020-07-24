using System.Collections.Generic;
using System.Threading.Tasks;
using Worker.Models;

namespace Worker
{
    public interface IWorkerRepository
    {
        public Task<IEnumerable<WorkerProfile>> Get();
        public Task<WorkerProfile> Get(int id);
        public Task Save(WorkerProfile worker);
        public Task Save(int id, WorkerProfile worker);
    }
}