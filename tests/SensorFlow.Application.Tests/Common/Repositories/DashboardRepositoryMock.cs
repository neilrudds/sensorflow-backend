using ErrorOr;
using SensorFlow.Application.Common.Interfaces;
using SensorFlow.Domain.Entities.Dashboards;
using System.Collections.ObjectModel;

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
            return Task.FromResult<ICollection<Dashboard>>(new Collection<Dashboard>
            {
                new Dashboard() { Name ="New Dashboard 1", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 2", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 3", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 4", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 5", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 6", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 7", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 8", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 9", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                new Dashboard() { Name ="New Dashboard 10", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
            });
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

            if (dashboardId.Equals("fc966bd8-bae2-418a-8617-4a2080644a99"))
            {
                return Task.FromResult<ErrorOr<Dashboard>>(new Dashboard
                {
                    Name = "New Dashboard II",
                    WorkspaceId = "5000"
                });
            }

            return Task.FromResult<ErrorOr<Dashboard>>(Error.NotFound("Dashboard not found"));
        }

        public Task<ErrorOr<List<Dashboard>>> GetDashboardsByWorkspaceIdAsync(CancellationToken cancellationToken, string workspaceId)
        {
            if (workspaceId.Equals("fc966bd8-bae2-418a-8617-4a2080644a75"))
            {
                return Task.FromResult<ErrorOr<List<Dashboard>>>(new List<Dashboard>
                {
                    new Dashboard() { Name ="New Dashboard 1", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 2", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 3", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 4", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 5", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 6", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 7", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 8", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 9", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                    new Dashboard() { Name ="New Dashboard 10", WorkspaceId = "5000", GridLayout = "{}", GridWidgets = "{}" },
                });
            }
            return Task.FromResult<ErrorOr<List<Dashboard>>>(Error.NotFound("WorkspaceId not found"));
        }

        public Task<Dashboard> UpdateDashboardAsync(CancellationToken cancellationToken, Dashboard toUpdate)
        {
            return Task.FromResult<Dashboard>(toUpdate);
        }
    }
}
