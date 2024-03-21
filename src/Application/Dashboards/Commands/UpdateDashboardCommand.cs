using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
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
            {
                var widgetResult = dashboard.Value.UpdateWidgetLayout(request.gridWidgets);
                if (widgetResult.IsError) 
                {
                    return widgetResult.Errors;
                }
            }

            if (!String.IsNullOrEmpty(request.gridLayout))
            {
                var layoutResult = dashboard.Value.UpdateGridLayout(request.gridLayout);
                if (layoutResult.IsError)
                {
                    return layoutResult.Errors;
                }
            }

            return await _dashboardRepository.UpdateDashboardAsync(cancellationToken, dashboard.Value);
        }
    }
}