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
                .AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
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

            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddMaps(assembly);
            //});
            //mapperConfig.AssertConfigurationIsValid();

            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            //services.AddAutoMapper(mc =>
            //{
            //    mc.AddMaps(assembly);
            //});

            services.AddAutoMapper(config =>
            {
                config.AddProfile<AppProfile>();
                //config.AddProfile<UserProfile>();
            });

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