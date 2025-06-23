using AutoMapper;
using LifeInsurance.API.DTOs;
using LifeInsurance.Application.Persons.Commands;
using LifeInsurance.Application.Persons.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeInsurance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PersonsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePersonDto request)
        {
            var command = _mapper.Map<CreatePersonCommand>(request);
            var result = await _mediator.Send(command);

            return CreatedAtRoute(
                routeName: "GetPersonById",
                routeValues: new { id = result },
                value: result);
        }

        [HttpGet("{id:guid}", Name = "GetPersonById")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery { Id = id });

            if (person is null) return NotFound();

            var dto = _mapper.Map<PersonDto>(person);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var persons = await _mediator.Send(new GetAllPersonsQuery());

            var dtos = _mapper.Map<List<PersonDto>>(persons);

            return Ok(dtos);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdatePersonDto request)
        {
            var command = _mapper.Map<UpdatePersonCommand>(request);
            command.Id = id;

            var success = await _mediator.Send(command);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _mediator.Send(new DeletePersonCommand { Id = id });
            return result ? NoContent() : NotFound();
        }
    }
}
