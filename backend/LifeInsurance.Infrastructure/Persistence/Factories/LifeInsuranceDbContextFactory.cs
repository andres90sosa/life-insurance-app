using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LifeInsurance.Infrastructure.Persistence.Factories
{
    public class LifeInsuranceDbContextFactory : IDesignTimeDbContextFactory<LifeInsuranceDbContext>
    {
        public LifeInsuranceDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LifeInsuranceDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new LifeInsuranceDbContext(optionsBuilder.Options);
        }
    }
}
