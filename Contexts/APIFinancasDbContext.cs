using System;
using System.Linq;
using APIFinancas.Interfaces;
using APIFinancas.Models;
using Microsoft.EntityFrameworkCore;

namespace APIFinancas.Contexts
{
    public class APIFinancasDbContext : DbContext
    {
        //Database Table Maps
        public DbSet<User> Users { get; set; }

        //Connection Strings
        public string DbServerName { get; }
        public string DbDatabaseName { get; }

        public APIFinancasDbContext()
        {
            DbServerName = "localhost";
            DbDatabaseName = "Financas";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server = {DbServerName}; Database = {DbDatabaseName}; Trusted_Connection = False");
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
