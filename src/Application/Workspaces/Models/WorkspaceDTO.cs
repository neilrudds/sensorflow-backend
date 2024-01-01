using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Dashboards.Models;
using SensorFlow.Application.Devices.Models;

namespace SensorFlow.Application.Workspaces.Models
{
    public sealed class WorkspaceDTO : EntityDTO
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public int deviceCount { get; set; }
        public int userCount { get; set; }
        public string tenantId { get; set; }
        public List<DashboardDTO> dashboards { get; set; } // Is this best practice? Check it out.
        public List<DeviceDTO> devices { get; set; }
    }
}