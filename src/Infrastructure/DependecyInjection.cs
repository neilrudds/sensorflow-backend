using SensorFlow.Infrastructure.Persistence;
using SensorFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SensorFlow.Domain.Abstractions.Repositories;

namespace SensorFlow.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("SensorflowDatabase") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<SensorFlowDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IPersonRepository, PersonRepository>(); // Inject PersonRepository where IPersonRepository Type is requested.

            return services;
        }
    }
}