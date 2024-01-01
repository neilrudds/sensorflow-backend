using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Tenants.Models;
using SensorFlow.Application.Tenants.Queries;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;
using SensorFlow.Application.Common.Models;

namespace SensorFlow.WebApi.Controllers
{
    [Authorize(Roles = "Owner")]
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

        [HttpGet]
        [ProducesResponseType(typeof(List<TenantDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetTenantsQuery());
            return Ok(results);
        }
    }
}