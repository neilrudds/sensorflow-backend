using SensorFlow.Domain.Entities.Workspaces;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.Models;

namespace SensorFlow.Domain.Entities.Tenants
{
    public sealed class Tenant : Entity<string>
    {
        public string Name { get; set; } = string.Empty;

        public int WorkspaceCount { get; set; } = 0;

        public int UserCount { get; set; } = 0;

        // Navigation Properties
        public ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();

        public ICollection<User> Users { get; set; } = new List<User>();

        public Tenant() { 
            Id = Guid.NewGuid().ToString();
        }

        private Tenant(string name) : this()
        {
            Name = name;
        }

        public static Tenant CreateTenant(string name)
        {
            var tenant = new Tenant(ValidateName(name));
            return tenant;
        }

        public static string ValidateName(string? name)
        {
            name = (name ?? string.Empty).Trim();
            return name;
        }
    }
}