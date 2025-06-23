using MediatR;

namespace LifeInsurance.Application.Persons.Commands
{
    public class DeletePersonCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
