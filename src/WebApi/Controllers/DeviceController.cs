using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Devices.Models;
using SensorFlow.Application.Devices.Queries;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;
using SensorFlow.Application.Devices.Commands;

namespace SensorFlow.WebApi.Controllers
{
    [Authorize(Roles = "Owner")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class DevicesController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IMediator _mediator;

        public DevicesController(ILogger<DevicesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<DeviceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetDeviceQuery(id));
            return Ok(result);
        }

        [HttpGet("getByWorkspaceId/{workspaceId}")]
        [ProducesResponseType(typeof(List<DeviceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByWorkspaceId(string workspaceId)
        {
            var result = await _mediator.Send(new GetWorkspaceDevicesQuery(workspaceId));

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] DeviceCreateDTO device)
        {
            var result = await _mediator.Send(new CreateDeviceCommand(device.id, device.name, device.fields, device.workspaceId, device.gatewayId));

            if (result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<DeviceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] DeviceUpdateDTO device)
        {
            var result = await _mediator.Send(new UpdateDeviceCommand(id, device.name, device.fields, device.gatewayId));

            if (result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteDeviceCommand(id));
            return NoContent();
        }
    }
}