using MediatR;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Application.Common.Interfaces;
using ErrorOr;

namespace SensorFlow.Application.Dashboards.Commands
{
    // Command
    public record CreateDashboardCommand(string name, string workspaceId) : IRequest<ErrorOr<Dashboard>>;

    // Command Handler
    public class CreateDashboardCommandHandler : IRequestHandler<CreateDashboardCommand, ErrorOr<Dashboard>>
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateDashboardCommandHandler(IDashboardRepository dashboardRepository, IWorkspaceRepository workspaceRepository)
        {
            // Repositories will be injected via dependancy injection
            _dashboardRepository = dashboardRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<ErrorOr<Dashboard>> Handle(CreateDashboardCommand request, CancellationToken cancellationToken)
        {
            // Check that the workspace exists first
            var workspace = await _workspaceRepository.GetWorkspaceByIdAsync(cancellationToken, request.workspaceId);

            // Return errors if it doesn't
            if (workspace.IsError)
                return workspace.Errors;

            // Otherwise create a new dashboard object
            var dashboard = Dashboard.CreateDashboard(
                request.name,
                request.workspaceId
            );

            // Add the dashboard object to the database
            var result = await _dashboardRepository.AddDashboardAsync(cancellationToken, dashboard.Value);

            // If errors occur, return to the presentation layer
            if (result.IsError)
                return result.Errors;

            // Otherwise, sucess, return dashboard object which was created
            return dashboard;
        }
    }
}