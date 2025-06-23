using LifeInsurance.Domain.Entities;
using LifeInsurance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LifeInsurance.Infrastructure.Data
{
    public class ApplicationDbSeeder
    {
        public static async Task SeedAsync(LifeInsuranceDbContext context)
        {
            if (await context.Persons.AnyAsync()) return;

            var persons = new List<Person>
            {
                new Person
                {
                    Id = Guid.NewGuid(),
                    FullName = "Andrés Sosa",
                    Identification = "30123456",
                    Age = 35,
                    Gender = "Masculino",
                    Drives = true,
                    UsesGlasses = false,
                    IsDiabetic = false,
                    OtherDiseases = string.Empty,
                    IsActive = true
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    FullName = "María Gómez",
                    Identification = "40111222",
                    Age = 42,
                    Gender = "Femenino",
                    Drives = false,
                    UsesGlasses = true,
                    IsDiabetic = true,
                    OtherDiseases = "Hipertensión",
                    IsActive = true
                }
            };

            context.Persons.AddRange(persons);
            await context.SaveChangesAsync();
        }
    }
}
