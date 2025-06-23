using LifeInsurance.Domain.Entities;

namespace LifeInsurance.Application.Persons.Services
{
    public interface IPersonService
    {
        Task<Guid> CreateAsync(Person person, CancellationToken cancellationToken);
        Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Person>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
