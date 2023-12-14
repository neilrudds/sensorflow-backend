using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Persons.Models;
using SensorFlow.Application.Persons.Queries;
using SensorFlow.Application.Persons.Commands;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;

namespace SensorFlow.WebApi.Controllers
{
    [Authorize (Roles = "Owner")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IMediator _mediator;

        public PersonsController(ILogger<PersonsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<PersonDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetPersonQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PersonDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetPersonsQuery());
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] PersonCreateDTO person)
        {
            var id = await _mediator.Send(new CreatePersonCommand(person.Name, person.Email, person.Phone));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelope(id.ToString())); // Is this good practice...
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] PersonUpdateDTO person)
        {
            await _mediator.Send(new UpdatePersonCommand(id, person.Name, person.Email, person.Phone));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePersonCommand(id));
            return NoContent();
        }
    }
}