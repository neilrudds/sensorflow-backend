using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Dashboards.Models;

namespace SensorFlow.Application.Workspaces.Models
{
    public sealed class WorkspaceDTO : EntityDTO
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public List<DashboardDTO> dashboards { get; set; } // Is this best practice? Check it out.
    }
}