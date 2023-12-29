using SensorFlow.Application.Common.Models;
using SensorFlow.Domain.Entities.Dashboards;

/* This interface will be used by the API as an abstraction of the repository whose implementation resides in the Infrastructure project. */
namespace SensorFlow.Application.Common.Interfaces
{
    public interface IDashboardRepository
    {
        Task<ICollection<Dashboard>> GetAllAsync(CancellationToken cancellationToken);

        Task<Dashboard> GetDashboardByIdAsync(CancellationToken cancellationToken, string dashboardId);

        Task<(Result result, Dashboard dashboard)> AddDashboardAsync(CancellationToken cancellationToken, Dashboard toCreate);

        Task<Dashboard> UpdateDashboardAsync(CancellationToken cancellationToken, string dashboardId, string name);

        Task DeleteDashboardAsync(CancellationToken cancellationToken, Dashboard toDelete);
    }
}