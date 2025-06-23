using LifeInsurance.Application.Persons.Services;
using LifeInsurance.Domain.Entities;
using MediatR;

namespace LifeInsurance.Application.Persons.Queries
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Person?>
    {
        private readonly IPersonService _personService;

        public GetPersonByIdQueryHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<Person?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _personService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
