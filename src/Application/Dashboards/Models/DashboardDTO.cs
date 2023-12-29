using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Dashboards.Models
{
    public sealed class DashboardDTO : EntityDTO
    {
        public string id { get; set; }
        public string? name { get; set; }
        public string workspaceId { get; set; }
    }
}
