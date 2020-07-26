using System.Collections.Generic;
using System.Threading.Tasks;
using Worker.Models;

namespace Worker
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<WorkerProfile>> Get(string? creator = null);
        Task<WorkerProfile?> Get(int id, string? creator = null);
        Task<WorkerProfile> Save(Models.WorkerProfile worker, string creator, int id = 0);
    }
}