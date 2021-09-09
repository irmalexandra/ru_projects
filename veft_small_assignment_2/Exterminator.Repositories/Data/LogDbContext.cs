using Exterminator.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exterminator.Repositories.Data
{
    public class LogDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlite("Filename=../Exterminator.Repositories/LogDb.db");
        }
    }
}