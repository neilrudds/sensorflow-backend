using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Identity.Commands;
using SensorFlow.Application.Identity.Queries;
using SensorFlow.Application.Identity.Models;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using SensorFlow.Domain.Enumerations;
using Azure.Core;

namespace SensorFlow.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetUserQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] UserCreateDTO user)
        {
            var result = await _mediator.Send(new CreateUserCommand(
                user.userName,
                user.firstName,
                user.lastName,
                user.email,
                user.password,
                user.tenantId,
                new List<string> { RoleEnum.Owner.ToString() }
            ));

            if (result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await _mediator.Send(new LoginUserCommand(
                loginRequest
             ));

            // This is messy, fix it. Move the route to a new auth controller?
            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}