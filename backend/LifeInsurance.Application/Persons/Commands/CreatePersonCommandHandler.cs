using LifeInsurance.Application.Persons.Services;
using LifeInsurance.Domain.Entities;
using MediatR;

namespace LifeInsurance.Application.Persons.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
    {
        private readonly IPersonService _personService;

        public CreatePersonCommandHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                FullName = request.FullName,
                Identification = request.Identification,
                Age = request.Age,
                Gender = request.Gender,
                Drives = request.Drives,
                UsesGlasses = request.UsesGlasses,
                IsDiabetic = request.IsDiabetic,
                OtherDiseases = request.OtherDiseases
            };

            return await _personService.CreateAsync(person, cancellationToken);
        }
    }
}
