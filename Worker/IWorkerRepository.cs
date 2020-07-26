using System.Collections.Generic;
using System.Threading.Tasks;
using Worker.Models;

namespace Worker
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<WorkerProfile>> Get();
        Task<WorkerProfile> Get(int id);
        Task<WorkerProfile> Save(Models.WorkerProfile worker, int id = 0);
    }
}