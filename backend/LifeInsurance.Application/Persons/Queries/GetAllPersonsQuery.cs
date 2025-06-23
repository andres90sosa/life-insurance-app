using LifeInsurance.Domain.Entities;
using MediatR;

namespace LifeInsurance.Application.Persons.Queries
{
    public class GetAllPersonsQuery : IRequest<List<Person>> {}
}
