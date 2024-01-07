using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Application.Workspaces.Models;
using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Enumerations;
using System.Data;
using System.Threading;

namespace SensorFlow.Application.Tenants.Commands
{
    // Command
    public record CreateTenantCommand(string name, UserCreateDTO user, WorkspaceCreateDTO workspace) : IRequest<(Result result, Tenant tenant)>;

    // Command Handler
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, (Result result, Tenant tenant)>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IApplicationUserService _applicationUserService;

        public CreateTenantCommandHandler(ITenantRepository tenantRepository, IApplicationUserService applicationUserService)
        {
            _tenantRepository = tenantRepository;
            _applicationUserService = applicationUserService;
        }

        public async Task<(Result result, Tenant tenant)> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var roles = new List<string> { nameof(RoleEnum.Owner) };
            var user = User.CreateUser(request.user.userName, request.user.firstName, request.user.lastName, request.user.email);
            await _applicationUserService.CreateUserAsync(user, request.user.password, roles, true);

            var tenant = new Tenant
            {
                Name = request.name,
                OwnerId = user.Id
            };
            tenant.Users.Add(user);

            var workspace = new Workspace
            { 
                Name = request.workspace.name,
                Tenant = tenant,
                OwnerId = user.Id
            };
            workspace.Users.Add(user);

            tenant.Workspaces.Add(workspace);

            return await _tenantRepository.AddTenantAsync(cancellationToken, tenant);
        }
    }
}