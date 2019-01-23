using System;
using System.Linq;
using APIFinances.Interfaces;
using APIFinances.Models;
using Microsoft.EntityFrameworkCore;

namespace APIFinances.Contexts
{
    public class APIFinancesDbContext : DbContext
    {
        //Database Table Maps
        public DbSet<User> Users { get; set; }

        //Connection Strings
        public string DbServerName { get; }
        public string DbDatabaseName { get; }
        public string DbUsername { get; }
        public string DbPassword { get; }

        public APIFinancesDbContext(DbContextOptions<APIFinancesDbContext> dbContextOptions) : base(dbContextOptions)
        {
            DbServerName = "localhost";
            DbDatabaseName = "Finances";
            DbUsername = "SA";
            DbPassword = "Dev4you@2018";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server = {DbServerName}; Database = {DbDatabaseName}; User Id = {DbUsername}; Password = {DbPassword} Trusted_Connection=False");
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            var newEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Added &&
                    x.Entity != null &&
                    x.Entity as ITimeStampedModel != null
                    )
                .Select(x => x.Entity as ITimeStampedModel);

            var modifiedEntities = this.ChangeTracker.Entries() 
                .Where(
                    x => x.State == EntityState.Modified &&
                    x.Entity != null &&
                    x.Entity as ITimeStampedModel != null
                    )
                .Select(x => x.Entity as ITimeStampedModel);

            foreach (var newEntity in newEntities)
            {
                newEntity.CreatedAt = DateTime.UtcNow;
                newEntity.LastModified = DateTime.UtcNow;
            }

            foreach (var modifiedEntity in modifiedEntities)
            {
                modifiedEntity.LastModified = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
    }    
}
