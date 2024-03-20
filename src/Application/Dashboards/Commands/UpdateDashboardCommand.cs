using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Dashboards;

namespace SensorFlow.Application.Dashboards.Commands
{
    public record UpdateDashboardCommand(string dashboardId, string? gridWidgets, string? gridLayout) : IRequest<ErrorOr<Dashboard>>;

    public class UpdateDashboardCommandHandler : IRequestHandler<UpdateDashboardCommand, ErrorOr<Dashboard>>
    {

        private readonly IDashboardRepository _dashboardRepository;

        public UpdateDashboardCommandHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<ErrorOr<Dashboard>> Handle(UpdateDashboardCommand request, CancellationToken cancellationToken)
        {
            var dashboard = await _dashboardRepository.GetDashboardByIdAsync(cancellationToken, request.dashboardId);

            if (dashboard.IsError)
                return dashboard.Errors;
            
            if (!String.IsNullOrEmpty(request.gridWidgets))
                dashboard.Value.UpdateWidgetLayout(request.gridWidgets);

            if (!String.IsNullOrEmpty(request.gridLayout))
                dashboard.Value.UpdateGridLayout(request.gridLayout);
            
            return await _dashboardRepository.UpdateDashboardAsync(cancellationToken, dashboard.Value);
        }
    }
}