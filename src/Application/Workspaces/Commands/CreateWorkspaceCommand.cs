using MediatR;
using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Workspaces.Commands
{
    // Command
    public record CreateWorkspaceCommand(string name) : IRequest<(Result result, Workspace workspace)>;

    // Command Handler
    public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, (Result result, Workspace workspace)>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateWorkspaceCommandHandler(IWorkspaceRepository repository)
        {
            _workspaceRepository = repository;
        }

        public async Task<(Result result, Workspace workspace)> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var workspace = Workspace.CreateWorkspace(
                request.name
            );

            return await _workspaceRepository.AddWorkspaceAsync(cancellationToken, workspace);
        }
    }
}