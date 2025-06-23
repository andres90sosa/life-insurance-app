using LifeInsurance.Domain.Entities;
using LifeInsurance.Domain.Interfaces;

namespace LifeInsurance.Application.Persons.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Guid> CreateAsync(Person person, CancellationToken cancellationToken)
        {
            person.Id = Guid.NewGuid();
            person.IsActive = true;

            await _personRepository.AddAsync(person, cancellationToken);
            return person.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(id, cancellationToken);
            if (person is null) return false;

            await _personRepository.DeleteAsync(person, cancellationToken);
            return true;
        }

        public async Task<List<Person>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _personRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _personRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken)
        {
            var existingPerson = await _personRepository.GetByIdAsync(person.Id, cancellationToken);
            if (existingPerson is null) return false;

            existingPerson.FullName = person.FullName;
            existingPerson.Identification = person.Identification;
            existingPerson.Age = person.Age;
            existingPerson.Gender = person.Gender;
            existingPerson.IsActive = person.IsActive;
            existingPerson.Drives = person.Drives;
            existingPerson.UsesGlasses = person.UsesGlasses;
            existingPerson.IsDiabetic = person.IsDiabetic;
            existingPerson.OtherDiseases = person.OtherDiseases;

            await _personRepository.UpdateAsync(existingPerson, cancellationToken);
            return true;
        }
    }
}
