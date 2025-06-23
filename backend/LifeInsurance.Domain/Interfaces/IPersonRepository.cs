using LifeInsurance.Domain.Entities;

namespace LifeInsurance.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person, CancellationToken cancellationToken);
        Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Person>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(Person person, CancellationToken cancellationToken);
        Task DeleteAsync(Person person, CancellationToken cancellationToken);
    }
}
