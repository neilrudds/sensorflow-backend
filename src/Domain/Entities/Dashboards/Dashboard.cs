using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Models;

namespace SensorFlow.Domain.Entities.Dashboards
{
    public sealed class Dashboard : Entity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Workspace Workspace { get; set; } // Required foreign key property, required relationship as not nullable.
        public Guid WorkspaceId { get; set; }

        private Dashboard()
        {

        }

        public Dashboard(Guid dashboardId, string name, Guid workspaceId, DateTime addedTime, DateTime lastModified)
        {
            Id = dashboardId;
            Name = name;
            WorkspaceId = workspaceId;
            AddedTime = addedTime;
            LastModified = lastModified;
        }

        public static Dashboard CreateDashboard(Guid dashboardId, string name, Guid workspaceId, DateTime addedTime, DateTime lastModified)
        {
            // Do I need validation on workspaceId?
            var dashboard = new Dashboard(dashboardId, ValidateName(name), workspaceId ,addedTime, lastModified);
            return dashboard;
        }

        public void Update(string name, Workspace workspace, DateTime addedTime, DateTime lastModified)
        {
            // Do I need validation on workspaceId?
            Name = ValidateName(name);
            Workspace = workspace;
            AddedTime = addedTime;
            LastModified = lastModified;
        }

        public static string ValidateName(string? name)
        {
            name = (name ?? string.Empty).Trim();
            return name;
        }
    }
}
