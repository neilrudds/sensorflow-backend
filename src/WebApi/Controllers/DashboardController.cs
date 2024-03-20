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
            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DashboardDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetDashboardsQuery());
            return Ok(results);
        }

        [HttpGet("getByWorkspaceId/{workspaceId}")]
        [ProducesResponseType(typeof(List<DashboardDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByWorkspaceId(string workspaceId)
        {
            var result = await _mediator.Send(new GetWorkspaceDashboardsQuery(workspaceId));

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] DashboardCreateDTO dashboard)
        {
            var result = await _mediator.Send(new CreateDashboardCommand(dashboard.name, dashboard.workspaceId));

            if (result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<DashboardDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] DashboardUpdateDTO dashboard)
        {
            var result = await _mediator.Send(new UpdateDashboardCommand(id, dashboard.GridWidgets, dashboard.GridLayout));

            if(result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Delete(Guid id)Dashboard
        //{
        //    await _mediator.Send(new DeletePersonCommand(id));
        //    return NoContent();
        //}
    }
}