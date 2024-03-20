﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Workspaces.Models;
using SensorFlow.Application.Workspaces.Queries;
using SensorFlow.Application.Workspaces.Commands;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;
using SensorFlow.Application.Common.Models;

namespace SensorFlow.WebApi.Controllers
{
    [Authorize(Roles = "Owner")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class WorkspacesController : ControllerBase
    {
        private readonly ILogger<WorkspacesController> _logger;
        private readonly IMediator _mediator;

        public WorkspacesController(ILogger<WorkspacesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkspaceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetWorkspaceQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<WorkspaceDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetWorkspacesQuery());
            return Ok(results);
        }

        [HttpGet("getByUsername/{username}")]
        [ProducesResponseType(typeof(List<WorkspaceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var result = await _mediator.Send(new GetUserWorkspacesQuery(username));

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WorkspaceDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] WorkspaceCreateDTO workspace)
        {
            var result = await _mediator.Send(new CreateWorkspaceCommand(workspace.name, workspace.tenantId, workspace.userName));

            if (result.IsError)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(Get), new { result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
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