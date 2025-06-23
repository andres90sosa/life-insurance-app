using LifeInsurance.Domain.Entities;
using LifeInsurance.Domain.Interfaces;
using LifeInsurance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LifeInsurance.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly LifeInsuranceDbContext _dbContext;

        public PersonRepository(LifeInsuranceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Person person, CancellationToken cancellationToken)
        {
            await _dbContext.Persons.AddAsync(person, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Person person, CancellationToken cancellationToken)
        {
            _dbContext.Persons.Remove(person);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Person>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Persons.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Persons.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Person person, CancellationToken cancellationToken)
        {
            _dbContext.Persons.Update(person);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
