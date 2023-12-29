using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Workspaces;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
namespace SensorFlow.Application.Common.Interfaces
{
    public interface IWorkspaceRepository
    {
        Task<ICollection<Workspace>> GetAllAsync(CancellationToken cancellationToken);

        Task<Workspace> GetWorkspaceByIdAsync(CancellationToken cancellationToken, string workspaceId);

        Task<(Result result, Workspace workspace)> AddWorkspaceAsync(CancellationToken cancellationToken, Workspace toCreate);

        Task<Workspace> UpdateWorkspaceAsync(CancellationToken cancellationToken, string workspaceId, string name);

        Task DeleteWorkspaceAsync(CancellationToken cancellationToken, Workspace toDelete);
    }
}