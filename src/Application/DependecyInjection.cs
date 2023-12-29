using FluentValidation;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SensorFlow.Application.Identity.MappingProfiles;
using SensorFlow.Application.Persons.MappingProfiles;
using SensorFlow.Application.Workspaces.MappingProfiles;
using SensorFlow.Application.Dashboards.MappingProfiles;
using SensorFlow.Application.Devices.MappingProfiles;

namespace SensorFlow.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependecyInjection).Assembly;

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            // This doesnt quite work here as the profiles in the infra layer are not being mapped...
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddMaps(assembly);
            //});
            //mapperConfig.AssertConfigurationIsValid();

            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            ////services.AddAutoMapper(mc =>
            ////{
            ////    mc.AddMaps(assembly);
            ////});
            ///

            services.AddAutoMapper(config =>
            {
                config.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
                config.AddProfile<UserProfile>();
                config.AddProfile<PersonProfile>();
                config.AddProfile<WorkspaceProfile>();
                config.AddProfile<DashboardProfile>();
                config.AddProfile<DeviceProfile>();
            });

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}