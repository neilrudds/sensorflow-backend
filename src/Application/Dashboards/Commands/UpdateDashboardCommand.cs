using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Domain.Entities.Workspaces;
using System.Reflection.Metadata.Ecma335;

namespace SensorFlow.Application.Dashboards.Commands
{
    public record UpdateDashboardCommand(string dashboardId, string? gridWidgets, string? gridLayout) : IRequest<(Result result, Dashboard dashboard)>;

    public class UpdateDashboardCommandHandler : IRequestHandler<UpdateDashboardCommand, (Result result, Dashboard dashboard)>
    {

        private readonly IDashboardRepository _dashboardRepository;

        public UpdateDashboardCommandHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<(Result result, Dashboard dashboard)> Handle(UpdateDashboardCommand request, CancellationToken cancellationToken)
        {
            var dashboard = _dashboardRepository.GetDashboardByIdAsync(cancellationToken, request.dashboardId);

            if (dashboard == null)
                return (Result.Failure("Dashboard not found."), new Dashboard { });
            
            if (!String.IsNullOrEmpty(request.gridWidgets))
                dashboard.Result.dashboard.UpdateWidgetLayout(request.gridWidgets);

            if (!String.IsNullOrEmpty(request.gridLayout))
                dashboard.Result.dashboard.UpdateGridLayout(request.gridLayout);
            
            return await _dashboardRepository.UpdateDashboardAsync(cancellationToken, dashboard.Result.dashboard);
        }
    }
}