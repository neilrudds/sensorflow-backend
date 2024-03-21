using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SensorFlow.Application.Identity.MappingProfiles;
using SensorFlow.Application.Workspaces.MappingProfiles;
using SensorFlow.Application.Dashboards.MappingProfiles;
using SensorFlow.Application.Devices.MappingProfiles;
using SensorFlow.Application.Gateways.MappingProfiles;
using SensorFlow.Application.Tenants.MappingProfiles;
using SensorFlow.Application.Common.Behaviours;

namespace SensorFlow.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            
            services.AddAutoMapper(config =>
            {
                config.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
                config.AddProfile<UserProfile>();
                config.AddProfile<TenantProfile>();
                config.AddProfile<WorkspaceProfile>();
                config.AddProfile<DashboardProfile>();
                config.AddProfile<DeviceProfile>();
                config.AddProfile<GatewayProfile>();
            });

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

            return services;
        }
    }
}