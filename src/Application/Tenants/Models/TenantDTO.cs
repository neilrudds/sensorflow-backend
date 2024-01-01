using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Devices.Models;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Application.Workspaces.Models;

namespace SensorFlow.Application.Tenants.Models
{
    public sealed class TenantDTO : EntityDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public int workspaceCount { get; set; }
        public int wserCount { get; set; }
        public List<WorkspaceDTO> workspaces { get; set; }
        public List<UserDTO> users { get; set; }
    }
}
