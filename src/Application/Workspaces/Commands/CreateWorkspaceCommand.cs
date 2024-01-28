using MediatR;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Workspaces.Commands
{
    // Command
    public record CreateWorkspaceCommand(string name, string tenantId, string userName) : IRequest<(Result result, Workspace workspace)>;

    // Command Handler
    public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, (Result result, Workspace workspace)>
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IApplicationUserService _applicationUserService;

        public CreateWorkspaceCommandHandler(IWorkspaceRepository repository, IApplicationUserService applicationUserService)
        {
            _workspaceRepository = repository;
            _applicationUserService = applicationUserService;
        }

        public async Task<(Result result, Workspace workspace)> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _applicationUserService.GetUserByUserNameAsync(request.userName);

            if (!userResult.result.Succeeded)
                return (Result.Failure("UserId not found!"), new Workspace { });


            var workspace = Workspace.CreateWorkspace(
                request.name,
                request.tenantId
            );
            workspace.Users.Add(userResult.user);

            return await _workspaceRepository.AddWorkspaceAsync(cancellationToken, workspace);
        }
    }
}