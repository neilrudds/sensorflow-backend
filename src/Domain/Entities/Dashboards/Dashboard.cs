using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Models;

namespace SensorFlow.Domain.Entities.Dashboards
{
    public sealed class Dashboard : Entity<string>
    {
        public string Name { get; set; } = string.Empty;
        public string WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } // Required foreign key property, required relationship as not nullable.

        private Dashboard()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Dashboard(string name, string workspaceId) : this()
        {
            Name = name;
            WorkspaceId = workspaceId;
        }

        public static Dashboard CreateDashboard(string name, string workspaceId)
        {
            // Do I need validation on workspaceId?
            var dashboard = new Dashboard(ValidateName(name), workspaceId);
            return dashboard;
        }

        public void Update(string name, string workspaceId)
        {
            // Do I need validation on workspaceId?
            Name = ValidateName(name);
            WorkspaceId = workspaceId;
        }

        public static string ValidateName(string? name)
        {
            name = (name ?? string.Empty).Trim();
            return name;
        }
    }
}
