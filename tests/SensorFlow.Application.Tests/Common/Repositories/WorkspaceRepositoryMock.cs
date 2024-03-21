using ErrorOr;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Workspaces;

namespace SensorFlow.Application.Tests.Common.Repositories
{
    internal class WorkspaceRepositoryMock : IWorkspaceRepository
    {
        public Task<ErrorOr<Workspace>> AddWorkspaceAsync(CancellationToken cancellationToken, Workspace toCreate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorkspaceAsync(CancellationToken cancellationToken, Workspace toDelete)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Workspace>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<Workspace>> GetWorkspaceByIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            if (workspaceId.Equals("1")) {
                return Task.FromResult<ErrorOr<Workspace>>(new Workspace{
                    CreatedTimestamp = DateTime.UtcNow
                });
            }
            return Task.FromResult<ErrorOr<Workspace>>(Error.NotFound("Workspace not found"));
        }

        public Task<ErrorOr<List<Workspace>>> GetWorkspacesByUsernameAsync(CancellationToken cancellationToken, string username)
        {
            throw new NotImplementedException();
        }

        public Task<Workspace> UpdateWorkspaceAsync(CancellationToken cancellationToken, string workspaceId, string name)
        {
            throw new NotImplementedException();
        }
    }
}
