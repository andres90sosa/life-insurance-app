using LifeInsurance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeInsurance.Infrastructure.Persistence
{
    public class LifeInsuranceDbContext : DbContext
    {
        public LifeInsuranceDbContext(DbContextOptions<LifeInsuranceDbContext> options) : base(options) {}

        public DbSet<Person> Persons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LifeInsuranceDbContext).Assembly);
        }
    }
}
