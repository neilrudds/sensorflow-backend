using SensorFlow.Domain.Entities.Workspaces;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
namespace SensorFlow.Application.Common.Interfaces
{
    public interface IWorkspaceRepository
    {
        Task<ICollection<Workspace>> GetAllAsync(CancellationToken cancellationToken);

        Task<Workspace> GetWorkspaceByIdAsync(CancellationToken cancellationToken, Guid workspaceId);

        Task<Workspace> AddWorkspaceAsync(CancellationToken cancellationToken, Workspace toCreate);

        Task<Workspace> UpdateWorkspaceAsync(CancellationToken cancellationToken, Guid workspaceId, string name, DateTime lastModified);

        Task DeleteWorkspaceAsync(CancellationToken cancellationToken, Workspace toDelete);
    }
}