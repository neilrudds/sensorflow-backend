using SensorFlow.Domain.Entities.Dashboards;
using SensorFlow.Domain.Entities.Devices;
using SensorFlow.Domain.Entities.Tenants;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.Models;

namespace SensorFlow.Domain.Entities.Workspaces
{
    public sealed class Workspace : Entity<string>
    {
        public string Name { get; set; } = string.Empty;

        public int DeviceCount { get; set; } = 0;

        public int UserCount { get; set; } = 0;

        public string TenantId { get; set; }

        public Tenant Tenant { get; set; }

        // Navigation Properties
        public ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();

        public ICollection<Device> Devices { get; set; } = new List<Device>();

       public ICollection<User> Users { get; set; } = new List<User>();
        
        public Workspace()
        {
            Id = Guid.NewGuid().ToString();
            //Dashboards = new Collection<Dashboard>();
            //Devices = new Collection<Device>();
        }

        public Workspace(string name, string tenantId) : this()
        {
            Name = name;
            TenantId = tenantId;
        }

        public static Workspace CreateWorkspace(string name, string tenantId)
        {
            var workspace = new Workspace(ValidateName(name), tenantId);
            return workspace;
        }

        public void Update(string name)
        {
            // TenantId cannot be changed once set.
            Name = ValidateName(name);
        }

        public static string ValidateName(string? name)
        {
            name = (name ?? string.Empty).Trim();
            return name;
        }
    }
}
