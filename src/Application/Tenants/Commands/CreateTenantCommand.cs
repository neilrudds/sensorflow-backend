using ErrorOr;
using MediatR;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Application.Workspaces.Models;
using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Enumerations;

namespace SensorFlow.Application.Tenants.Commands
{
    // Command
    public record CreateTenantCommand(string name, UserCreateDTO user, TenantWorkspaceCreateDTO workspace) : IRequest<ErrorOr<Tenant>>;

    // Command Handler
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, ErrorOr<Tenant>>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IApplicationUserService _applicationUserService;

        public CreateTenantCommandHandler(ITenantRepository tenantRepository, IApplicationUserService applicationUserService)
        {
            _tenantRepository = tenantRepository;
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<Tenant>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var roles = new List<string> { nameof(RoleEnum.Owner) };
            var user = User.CreateUser(request.user.userName, request.user.firstName, request.user.lastName, request.user.email);
            var createUserResult = await _applicationUserService.CreateUserAsync(user, request.user.password, roles, true);

            if (createUserResult.IsError)
                return createUserResult.Errors;

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