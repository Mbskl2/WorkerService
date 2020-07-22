using Microsoft.EntityFrameworkCore;
using Worker.DAL.Models;

namespace Worker.DAL
{
    public class WorkerDbContext: DbContext
    {
        public DbSet<WorkerProfile> WorkerProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public WorkerDbContext(DbContextOptions<WorkerDbContext> options)
            : base(options)
        {
        }
    }
}