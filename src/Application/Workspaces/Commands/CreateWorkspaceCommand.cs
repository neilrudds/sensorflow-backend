using MediatR;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Application.Common.Interfaces;

namespace SensorFlow.Application.Workspaces.Commands
{
    // Command
    public record CreateWorkspaceCommand(string name) : IRequest<Guid>;

    // Command Handler
    public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, Guid>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateWorkspaceCommandHandler(IWorkspaceRepository repository)
        {
            _workspaceRepository = repository;
        }

        public async Task<Guid> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var workspace = Workspace.CreateWorkspace(
                Guid.NewGuid(),
                request.name,
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            await _workspaceRepository.AddWorkspaceAsync(cancellationToken, workspace);
            return workspace.Id;
        }
    }
}