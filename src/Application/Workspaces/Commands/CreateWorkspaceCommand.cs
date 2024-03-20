using MediatR;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Application.Common.Interfaces;
using ErrorOr;

namespace SensorFlow.Application.Workspaces.Commands
{
    // Command
    public record CreateWorkspaceCommand(string name, string tenantId, string userName) : IRequest<ErrorOr<Workspace>>;

    // Command Handler
    public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, ErrorOr<Workspace>>
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IApplicationUserService _applicationUserService;

        public CreateWorkspaceCommandHandler(IWorkspaceRepository repository, IApplicationUserService applicationUserService)
        {
            _workspaceRepository = repository;
            _applicationUserService = applicationUserService;
        }

        public async Task<ErrorOr<Workspace>> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserService.GetUserByUserNameAsync(request.userName);

            if (user is null)
                return Error.NotFound(description: "User Id not found");

            var workspace = Workspace.CreateWorkspace(
                request.name,
                request.tenantId
            );
            workspace.Users.Add(user);

            return await _workspaceRepository.AddWorkspaceAsync(cancellationToken, workspace);
        }
    }
}