using Microsoft.EntityFrameworkCore;
using Worker.DAL.Entities;

namespace Worker.DAL
{
#pragma warning disable 8618
    public class WorkerDbContext: DbContext
    {
        public DbSet<WorkerProfile> WorkerProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public WorkerDbContext(DbContextOptions<WorkerDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}