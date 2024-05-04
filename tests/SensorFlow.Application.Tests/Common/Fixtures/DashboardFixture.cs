using ErrorOr;
using FluentAssertions.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Dashboards.Commands;
using SensorFlow.Application.Dashboards.MappingProfiles;
using SensorFlow.Application.Dashboards.Models;
using SensorFlow.Application.Dashboards.Queries;
using SensorFlow.Application.Devices.MappingProfiles;
using SensorFlow.Application.Gateways.MappingProfiles;
using SensorFlow.Application.Identity.MappingProfiles;
using SensorFlow.Application.Tenants.MappingProfiles;
using SensorFlow.Application.Tests.Common.Repositories;
using SensorFlow.Application.Workspaces.MappingProfiles;
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

        public async Task<ErrorOr<DashboardDTO>> Send(GetDashboardQuery cmd)
        {
            var services = new ServiceCollection();
            var serviceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetDashboardQueryHandler).Assembly))
                .AddAutoMapper(cfg => {
                    cfg.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
                    cfg.AddProfile<DashboardProfile>();
                })
                .AddScoped<IDashboardRepository, DashboardRepositoryMock>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(cmd);
            return response;
        }

        public async Task<List<DashboardDTO>> Send(GetDashboardsQuery cmd)
        {
            var services = new ServiceCollection();
            var serviceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetDashboardsQueryHandler).Assembly))
                .AddAutoMapper(cfg => {
                    cfg.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
                    cfg.AddProfile<DashboardProfile>();
                })
                .AddScoped<IDashboardRepository, DashboardRepositoryMock>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(cmd);
            return response;
        }

        public async Task<ErrorOr<List<DashboardDTO>>> Send(GetWorkspaceDashboardsQuery cmd)
        {
            var services = new ServiceCollection();
            var serviceProvider = services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetWorkspaceDashboardsQueryHandler).Assembly))
                .AddAutoMapper(cfg => {
                    cfg.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
                    cfg.AddProfile<DashboardProfile>();
                })
                .AddScoped<IDashboardRepository, DashboardRepositoryMock>()
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            var response = await mediator.Send(cmd);
            return response;
        }
    }
}