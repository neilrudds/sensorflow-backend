using ErrorOr;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Dashboards;

namespace SensorFlow.Application.Tests.Common.Repositories
{
    public class DashboardRepositoryMock : IDashboardRepository
    {
        public Task<ErrorOr<Dashboard>> AddDashboardAsync(CancellationToken cancellationToken, Dashboard toCreate)
        {
            return Task.FromResult<ErrorOr<Dashboard>>(Dashboard.CreateDashboard(toCreate.Name, toCreate.WorkspaceId));
        }

        public Task DeleteDashboardAsync(CancellationToken cancellationToken, Dashboard toDelete)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Dashboard>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<Dashboard>> GetDashboardByIdAsync(CancellationToken cancellationToken, string dashboardId)
        {
            if (dashboardId.Equals("1"))
            {
                return Task.FromResult<ErrorOr<Dashboard>>(new Dashboard
                { 
                    Name = "New Dashboard", 
                    WorkspaceId = "1000" 
                });
            }
            return Task.FromResult<ErrorOr<Dashboard>>(Error.NotFound("Dashboard not found"));
        }

        public Task<ErrorOr<List<Dashboard>>> GetDashboardsByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            throw new NotImplementedException();
        }

        public Task<Dashboard> UpdateDashboardAsync(CancellationToken cancellationToken, Dashboard toUpdate)
        {
            return Task.FromResult<Dashboard>(toUpdate);
        }
    }
}
