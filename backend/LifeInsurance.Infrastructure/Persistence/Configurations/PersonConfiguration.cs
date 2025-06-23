using LifeInsurance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LifeInsurance.Infrastructure.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Identification).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Age).IsRequired();
            builder.Property(p => p.Gender).IsRequired().HasMaxLength(20);
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.OtherDiseases).HasMaxLength(200);
        }
    }
}
