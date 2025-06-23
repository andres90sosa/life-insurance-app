using LifeInsurance.Application.Persons.Services;
using LifeInsurance.Domain.Entities;
using MediatR;

namespace LifeInsurance.Application.Persons.Commands
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonService _personService;

        public UpdatePersonCommandHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                Id = request.Id,
                FullName = request.FullName,
                Identification = request.Identification,
                Age = request.Age,
                Gender = request.Gender,
                IsActive = request.IsActive,
                Drives = request.Drives,
                UsesGlasses = request.UsesGlasses,
                IsDiabetic = request.IsDiabetic,
                OtherDiseases = request.OtherDiseases,
            };

            return await _personService.UpdateAsync(person, cancellationToken);
        }
    }
}
