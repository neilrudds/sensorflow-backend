using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Gateways.Models;
using SensorFlow.Application.Gateways.Queries;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;
using SensorFlow.Application.Gateways.Commands;

namespace SensorFlow.WebApi.Controllers
{
    [Authorize(Roles = "Owner")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class GatewaysController : ControllerBase
    {
        private readonly ILogger<GatewaysController> _logger;
        private readonly IMediator _mediator;

        public GatewaysController(ILogger<GatewaysController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<GatewayDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetGatewayQuery(id));

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("getByWorkspaceId/{workspaceId}")]
        [ProducesResponseType(typeof(List<GatewayDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByWorkspaceId(string workspaceId)
        {
            var result = await _mediator.Send(new GetWorkspaceGatewaysQuery(workspaceId));

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] GatewayCreateDTO gateway)
        {
            var result = await _mediator.Send(new CreateGatewayCommand(gateway.name, gateway.workspaceId, gateway.host, gateway.portNumber, gateway.sSLEnabled));

            if (result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<GatewayDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] GatewayUpdateDTO gateway)
        {
            var result = await _mediator.Send(new UpdateGatewayCommand(id, gateway.name, gateway.host, gateway.portNumber, gateway.username, gateway.password, gateway.clientId, gateway.sSLEnabled));

            if (result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteGatewayCommand(id));
            return NoContent();
        }
    }
}