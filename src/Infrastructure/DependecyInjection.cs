using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SensorFlow.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using SensorFlow.Infrastructure.Repositories;
using SensorFlow.Infrastructure.DbContexts;
using SensorFlow.Infrastructure.Services.Auth;
using SensorFlow.Infrastructure.Models.Identity;
using SensorFlow.Infrastructure.Services.DbInit;
using AutoMapper;

namespace SensorFlow.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependecyInjection).Assembly;

            var connectionString =
                configuration.GetConnectionString("SensorflowDatabase") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<SensorFlowDbContext>(options => options.UseSqlServer(connectionString));

            services
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<SensorFlowDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(assembly);
            });
            //mapperConfig.AssertConfigurationIsValid();

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IPersonRepository, PersonRepository>(); // Inject PersonRepository where IPersonRepository Type is requested.
            services.AddScoped<IApplicationUserService, ApplicationUserService>();

            return services;
        }
    }
}