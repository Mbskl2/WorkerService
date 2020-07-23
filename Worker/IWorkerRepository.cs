using System.Collections.Generic;
using System.Threading.Tasks;
using Worker.Models;

namespace Worker
{
    public interface IWorkerRepository
    {
        public Task<IEnumerable<IWorkerProfile>> Get();
        public Task<IWorkerProfile> Get(int id);
        public Task Save(IWorkerProfile worker);
        public Task Save(int id, IWorkerProfile worker);
    }
}