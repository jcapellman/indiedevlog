using indiedevlog.web.EFModel.Objects;

using Microsoft.EntityFrameworkCore;

namespace indiedevlog.web.EFModel
{
    public class EntityFactory : DbContext
    {
        public DbSet<PlanUpdates> PlanUpdates { get; set; }

        public DbSet<Users> Users { get; set; }

        private readonly string _connectionString;

        public EntityFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}