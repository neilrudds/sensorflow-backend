using MediatR;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Dashboards.Commands
{
    // Command
    public record CreateDashboardCommand(string name, string workspaceId) : IRequest<(Result result, Dashboard dashboard)>;

    // Command Handler
    public class CreateDashboardCommandHandler : IRequestHandler<CreateDashboardCommand, (Result result, Dashboard dashboard)>
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IWorkspaceRepository _workspaceRepository;


        public CreateDashboardCommandHandler(IDashboardRepository dashboardRepository, IWorkspaceRepository workspaceRepository)
        {
            _dashboardRepository = dashboardRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<(Result result, Dashboard? dashboard)> Handle(CreateDashboardCommand request, CancellationToken cancellationToken)
        {
            //var workspace = await _workspaceRepository.GetWorkspaceByIdAsync(cancellationToken, request.workspaceId);

            //if (workspace is null)
            //    return (Result.Failure("Unable to locate workspace"),  null);

            var dashboard = Dashboard.CreateDashboard(
                request.name,
                request.workspaceId
            );

            return await _dashboardRepository.AddDashboardAsync(cancellationToken, dashboard);
        }
    }
}