using LifeInsurance.Application.Persons.Services;
using MediatR;

namespace LifeInsurance.Application.Persons.Commands
{
    internal class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonService _personService;

        public DeletePersonCommandHandler(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            return await _personService.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
