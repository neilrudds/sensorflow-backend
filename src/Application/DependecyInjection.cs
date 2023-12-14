using FluentValidation;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SensorFlow.Application.Identity.MappingProfiles;
using SensorFlow.Application.Persons.MappingProfiles;

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
            });

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}