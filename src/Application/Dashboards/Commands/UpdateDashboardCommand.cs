using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Dashboards;

namespace SensorFlow.Application.Dashboards.Commands
{
    public record UpdateDashboardCommand(string dashboardId, string name) : IRequest<Dashboard>;

    public class UpdateDashboardCommandHandler : IRequestHandler<UpdateDashboardCommand, Dashboard>
    {

        private readonly IDashboardRepository _dashboardRepository;

        public UpdateDashboardCommandHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<Dashboard> Handle(UpdateDashboardCommand request, CancellationToken cancellationToken)
        {
            return await _dashboardRepository.UpdateDashboardAsync(
                cancellationToken,
                request.dashboardId,
                request.name);
        }
    }
}