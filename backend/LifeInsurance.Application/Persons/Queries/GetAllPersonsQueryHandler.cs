using LifeInsurance.Application.Persons.Services;
using LifeInsurance.Domain.Entities;
using MediatR;

namespace LifeInsurance.Application.Persons.Queries
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<Person>>
    {
        private readonly IPersonService _personService;

        public GetAllPersonsQueryHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<List<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return await _personService.GetAllAsync(cancellationToken);
        }
    }
}
