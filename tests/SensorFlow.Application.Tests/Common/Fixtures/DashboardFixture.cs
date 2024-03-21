using ErrorOr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Dashboards.Commands;
using SensorFlow.Application.Tests.Common.Repositories;
using SensorFlow.Domain.Entities.Dashboards;

namespace SensorFlow.Application.Tests.Common.Fixtures
{
    public class DashboardFixture
    {
        public async Task<ErrorOr<Dashboard>> Send(CreateDashboardCommand cmd)
        {
            var services = new ServiceCollection();
            var serviceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateDashboardCommandHandler).Assembly))
                .AddScoped<IDashboardRepository, DashboardRepositoryMock>()
                .AddScoped<IWorkspaceRepository, WorkspaceRepositoryMock>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(cmd);
            return response;
        }

        public async Task<ErrorOr<Dashboard>> Send(UpdateDashboardCommand cmd)
        {
            var services = new ServiceCollection();
            var serviceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateDashboardCommandHandler).Assembly))
                .AddScoped<IDashboardRepository, DashboardRepositoryMock>()
                .AddScoped<IWorkspaceRepository, WorkspaceRepositoryMock>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(cmd);
            return response;
        }
    }
}
