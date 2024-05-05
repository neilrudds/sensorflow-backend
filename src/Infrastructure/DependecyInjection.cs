using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SensorFlow.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using SensorFlow.Infrastructure.Repositories;
using SensorFlow.Infrastructure.DbContexts;
using SensorFlow.Infrastructure.Services.Auth;
using SensorFlow.Infrastructure.MappingProfiles;
using Microsoft.AspNetCore.Http;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Infrastructure
{
    // Inject Infrastructure Layer Dependencies

    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependecyInjection).Assembly;

            // Retrieve SQL server connection string from the appsettings.json file, throw an error if invalid
            var connectionString =
                configuration.GetConnectionString("SensorflowDatabase") ??
                throw new ArgumentNullException(nameof(configuration));

            // Set the SQL server connection string for our DbContext
            services.AddDbContext<SensorFlowDbContext>(options => options.UseSqlServer(connectionString));

            // Add the following services
            services
                .AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddEntityFrameworkStores<SensorFlowDbContext>();

            // Configure IdentifyFramework user Identity options
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
            });

            // Add an AutoMapper profile for AppProfile
            services.AddAutoMapper(config =>
            {
                config.AddProfile<AppProfile>();
            });

            // Add the dependicies for the following interfaces (i.e. map the concrete implementation of our services to the interface types).
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IGatewayRepository, GatewayRepository>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserResolverService>();

            return services;
        }
    }
}