using Microsoft.EntityFrameworkCore;
using Population.SQLite.App.Application.Common.Interfaces;
using Population.SQLite.App.Domain.Entities;

namespace Population.SQLite.App.Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public DbSet<Estimates> Estimate { get; set; }
        public DbSet<Actuals> Actual { get; set; }

        public ApplicationDBContext(DbContextOptions x) : base(x)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actuals>()
                .HasNoKey()
                .ToView("Actuals");
            modelBuilder.Entity<Estimates>()
                .HasNoKey()
                .ToView("Estimate");
        }
    }
}
