using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Domain.Models;
using System.Collections.ObjectModel;

namespace SensorFlow.Domain.Entities.Workspaces
{
    public sealed class Workspace : Entity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Dashboard> Dashboards { get; set; }
        
        public Workspace()
        {
            Dashboards = new Collection<Dashboard>();
        }

        public Workspace(Guid workspaceId, string name, DateTime addedTime, DateTime lastModified)
        {
            Id = workspaceId;
            Name = name;
            AddedTime = addedTime;
            LastModified = lastModified;
        }

        public static Workspace CreateWorkspace(Guid workspaceId, string name, DateTime addedTime, DateTime lastModified)
        {
            var workspace = new Workspace(workspaceId, ValidateName(name), addedTime, lastModified);
            return workspace;
        }

        public void Update(string name, DateTime addedTime, DateTime lastModified)
        {
            Name = ValidateName(name);
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
