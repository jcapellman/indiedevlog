using System;
using System.Linq;

using indiedevlog.web.EFModel.Objects;
using indiedevlog.web.EFModel.Objects.SPs;
using indiedevlog.web.EFModel.Objects.Tables;

using Microsoft.EntityFrameworkCore;

namespace indiedevlog.web.EFModel
{
    public class EntityFactory : DbContext
    {
        public DbSet<PlanUpdates> PlanUpdates { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Projects> Projects { get; set; }

        public DbSet<Users2Projects> Users2Projects { get; set; }

        public DbSet<getLatestPlanUpdatesSP> getLatestPlanUpdatesSP { get; set; }

        public DbSet<getUserProjectsSP> getUserProjectsSP { get; set; }

        private readonly string _connectionString;

        public EntityFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries();

            if (changeSet == null)
            {
                return base.SaveChanges();
            }

            foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Member("Created").CurrentValue = DateTime.Now;
                        entry.Member("Active").CurrentValue = true;
                        break;
                }
                    
                entry.Member("Modified").CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }
    }
}