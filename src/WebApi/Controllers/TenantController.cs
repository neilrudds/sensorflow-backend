using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Tenants.Models;
using SensorFlow.Application.Tenants.Queries;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;
using SensorFlow.Application.Tenants.Commands;

namespace SensorFlow.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class TenantsController : ControllerBase
    {
        private readonly ILogger<TenantsController> _logger;
        private readonly IMediator _mediator;

        public TenantsController(ILogger<TenantsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TenantDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetTenantQuery(id));
            return Ok(result);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        [ProducesResponseType(typeof(List<TenantDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetTenantsQuery());
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] TenantCreateDTO tenant)
        {
            var result = await _mediator.Send(new CreateTenantCommand(
                tenant.name,
                tenant.user,
                tenant.workspace
            ));

            if (!result.result.Succeeded)
                return BadRequest(result.result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.tenant.Id }, new CreatedResultEnvelope(result.tenant.Id));
        }
    }
}