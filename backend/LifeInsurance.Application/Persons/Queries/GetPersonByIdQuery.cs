using LifeInsurance.Domain.Entities;
using MediatR;

namespace LifeInsurance.Application.Persons.Queries
{
    public class GetPersonByIdQuery : IRequest<Person?>
    {
        public Guid Id { get; set; }
    }
}
