using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Models;
using System.Collections.ObjectModel;

namespace SensorFlow.Domain.Entities.Devices
{
    public sealed class Device : Entity<string>
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Workspace> Workspaces { get; set; }

        public Device() { 
            Id = Guid.NewGuid().ToString();
            Workspaces = new Collection<Workspace>();
        }

        public Device(string name, ICollection<Workspace> workspaces) : this()
        {
            Name = name;
            Workspaces = workspaces;
        }
    }
}
