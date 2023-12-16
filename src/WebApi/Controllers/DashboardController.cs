using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Dashboards.Models;
using SensorFlow.Application.Dashboards.Queries;
using SensorFlow.Application.Dashboards.Commands;
using SensorFlow.WebApi.Infrastructure.ActionResults;

namespace SensorFlow.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class DashboardsController : ControllerBase
    {
        private readonly ILogger<DashboardsController> _logger;
        private readonly IMediator _mediator;

        public DashboardsController(ILogger<DashboardsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<DashboardDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetDashboardQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DashboardDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetDashboardsQuery());
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DashboardDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] DashboardCreateDTO dashboard)
        {
            var id = await _mediator.Send(new CreateDashboardCommand(dashboard.name, dashboard.workspaceId));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelope(id.ToString())); // Is this good practice...
        }

        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Put(Guid id, [FromBody] PersonUpdateDTO person)
        //{
        //    await _mediator.Send(new UpdatePersonCommand(id, person.Name, person.Email, person.Phone));
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _mediator.Send(new DeletePersonCommand(id));
        //    return NoContent();
        //}
    }
}