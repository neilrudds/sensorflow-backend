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
    // Inject Application Layer Dependencies
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            // Add MediatR services and add ValidationBahaviour middleware
            services.AddMediatR(configuration =>
            {
                // Register all dependencies of DependencyInjection type from MediatR
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            
            // Add AutoMapper services, and apply all mapping profiles
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