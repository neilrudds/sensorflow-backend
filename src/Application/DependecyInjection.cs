using FluentValidation;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SensorFlow.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependecyInjection).Assembly;

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            var mapperConfig = new MapperConfiguration(mc => 
            { 
                mc.AddMaps(assembly);
            });
            mapperConfig.AssertConfigurationIsValid();

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}