using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Domain.Entities.Devices;
using SensorFlow.Domain.Models;
using System.Collections.ObjectModel;

namespace SensorFlow.Domain.Entities.Workspaces
{
    public sealed class Workspace : Entity<string>
    {
        public string Name { get; set; } = string.Empty;

        public int DeviceCount { get; set; } = 0;

        public int UserCount { get; set; } = 0;

        public ICollection<Dashboard> Dashboards { get; set; }

        public ICollection<Device> Devices { get; set; }
        
        public Workspace()
        {
            Id = Guid.NewGuid().ToString();
            Dashboards = new Collection<Dashboard>();
            Devices = new Collection<Device>();
        }

        public Workspace(string name) : this()
        {
            Name = name;
        }

        public static Workspace CreateWorkspace(string name)
        {
            var workspace = new Workspace(ValidateName(name));
            return workspace;
        }

        public void Update(string name)
        {
            Name = ValidateName(name);
        }

        public static string ValidateName(string? name)
        {
            name = (name ?? string.Empty).Trim();
            return name;
        }
    }
}
