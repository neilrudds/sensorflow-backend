using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Dashboards;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
namespace SensorFlow.Application.Common.Interfaces
{
    public interface IDashboardRepository
    {
        Task<ICollection<Dashboard>> GetAllAsync(CancellationToken cancellationToken);

        Task<(Result result, List<Dashboard> dashboards)> GetDashboardsByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId);

        Task<(Result result, Dashboard dashboard)> GetDashboardByIdAsync(CancellationToken cancellationToken, string dashboardId);

        Task<(Result result, Dashboard dashboard)> AddDashboardAsync(CancellationToken cancellationToken, Dashboard toCreate);

        Task<(Result result, Dashboard dashboard)> UpdateDashboardAsync(CancellationToken cancellationToken, Dashboard toUpdate);

        Task DeleteDashboardAsync(CancellationToken cancellationToken, Dashboard toDelete);
    }
}