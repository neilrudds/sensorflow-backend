using MediatR;
using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Application.Common.Interfaces;

namespace SensorFlow.Application.Dashboards.Commands
{
    // Command
    public record CreateDashboardCommand(string name, Guid workspaceId) : IRequest<Guid>;

    // Command Handler
    public class CreateDashboardCommandHandler : IRequestHandler<CreateDashboardCommand, Guid>
    {
        private readonly IDashboardRepository _dashboardRepository;

        public CreateDashboardCommandHandler(IDashboardRepository repository)
        {
            _dashboardRepository = repository;
        }

        public async Task<Guid> Handle(CreateDashboardCommand request, CancellationToken cancellationToken)
        {
            var dashboard = Dashboard.CreateDashboard(
                Guid.NewGuid(),
                request.name,
                request.workspaceId,
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            await _dashboardRepository.AddDashboardAsync(cancellationToken, dashboard);
            return dashboard.Id;
        }
    }
}