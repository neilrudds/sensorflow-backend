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
            // Repositories will be injected via dependancy injection
            _dashboardRepository = dashboardRepository;
        }
        public async Task<ErrorOr<Dashboard>> Handle(UpdateDashboardCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the dashboard by Id
            var dashboard = await _dashboardRepository.GetDashboardByIdAsync(cancellationToken, request.dashboardId);

            // If unable to retrieve, return errors
            if (dashboard.IsError)
                return dashboard.Errors;
            
            // If the gridWidgets property is not empty, update object
            if (!String.IsNullOrEmpty(request.gridWidgets))
            {
                var widgetResult = dashboard.Value.UpdateWidgetLayout(request.gridWidgets);
                if (widgetResult.IsError) 
                {
                    return widgetResult.Errors;
                }
            }

            // If the gridLayout property is not empty, update object
            if (!String.IsNullOrEmpty(request.gridLayout))
            {
                var layoutResult = dashboard.Value.UpdateGridLayout(request.gridLayout);
                if (layoutResult.IsError)
                {
                    return layoutResult.Errors;
                }
            }

            // After object properties updated, asyncronously save object to database, returning errors
            return await _dashboardRepository.UpdateDashboardAsync(cancellationToken, dashboard.Value);
        }
    }
}