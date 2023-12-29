using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Dashboards.Models;
using SensorFlow.Application.Dashboards.Queries;
using SensorFlow.Application.Dashboards.Commands;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;

namespace SensorFlow.WebApi.Controllers
{
    [Authorize(Roles = "Owner")]
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
        public async Task<IActionResult> Get(string id)
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
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] DashboardCreateDTO dashboard)
        {
            var result = await _mediator.Send(new CreateDashboardCommand(dashboard.name, dashboard.workspaceId));

            if (!result.result.Succeeded)
                return BadRequest(result.result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.dashboard.Id }, new CreatedResultEnvelope(result.dashboard.Id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] DashboardUpdateDTO dashboard)
        {
            await _mediator.Send(new UpdateDashboardCommand(id, dashboard.Name));
            return NoContent();
        }

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