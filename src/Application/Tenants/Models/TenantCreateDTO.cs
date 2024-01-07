using SensorFlow.Application.Identity.Models;
using SensorFlow.Application.Workspaces.Models;

namespace SensorFlow.Application.Tenants.Models
{
    public sealed class TenantCreateDTO
    {
        public string name { get; set; }
        public UserCreateDTO user { get; set; }
        public WorkspaceCreateDTO workspace { get; set; }       
    }
}