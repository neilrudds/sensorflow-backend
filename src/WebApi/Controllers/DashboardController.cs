using MediatR;
using Microsoft.AspNetCore.Mvc;
using SensorFlow.Application.Dashboards.Models;
using SensorFlow.Application.Dashboards.Queries;
using SensorFlow.Application.Dashboards.Commands;
using SensorFlow.WebApi.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;

namespace SensorFlow.WebApi.Controllers
{
    [Authorize(Roles = "Owner")] // Only the Owner is permitted to access this controller, unless overwritten by an individual API Verb Method
    [ApiController] // Class is derived from a standard .NET ApiController
    [Route("api/[controller]")] // Route is, api/Dashboards
    [Produces("application/json")] // Always return a JSON formatted response
    public sealed class DashboardsController : ControllerBase
    {
        private readonly ILogger<DashboardsController> _logger;
        private readonly IMediator _mediator;

        // Through dependancy injection, inject logger and mediator implementations via controller constructor
        public DashboardsController(ILogger<DashboardsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        // Handle GET Request with id paremater in URL Query String
        [HttpGet("{id}")] 
        [ProducesResponseType(typeof(List<DashboardDTO>), StatusCodes.Status200OK)] // If the request is sucessful, return a list of dashboards and a 200 OK HTTP response
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)] // If the record is not found, return a 404 Not Found HTTP response and error messages
        // Acyncronous Task, accepting the Dashboard is as a parameter
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetDashboardQuery(id)); // Through the mediator, request the GetDashboardQuery from the application layer
            if (result.IsError)
                return BadRequest(result.Errors); // Return errors if they exist. with a 404 Not Found

            return Ok(result.Value); // Otherwise, return a 200 OK with the Dashoard details
        }

        // Generic GET request, if not Id is provided, all Dashboards will be returned where the requestor has permission
        [HttpGet]
        [ProducesResponseType(typeof(List<DashboardDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetDashboardsQuery());
            return Ok(results);
        }

        // Return a list of dashboards which belong to a specific workspace by Id.
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

        // Create a new dashboard.
        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] DashboardCreateDTO dashboard)
        {
            var result = await _mediator.Send(new CreateDashboardCommand(dashboard.name, dashboard.workspaceId));

            if (result.IsError)
                return BadRequest(result.Errors);

            // If sucessfully created, perform a GET request and return all details of the created Dashboard for confirmation.
            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }

        // Update an existing dashboard, by dashboard id.
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<DashboardDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] DashboardUpdateDTO dashboard)
        {
            var result = await _mediator.Send(new UpdateDashboardCommand(id, dashboard.GridWidgets, dashboard.GridLayout));

            if(result.IsError)
                return BadRequest(result.Errors);

            // If sucessfully updated, perform a GET request and return all details of the created Dashboard for confirmation.
            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, new CreatedResultEnvelope(result.Value.Id));
        }
    }
}