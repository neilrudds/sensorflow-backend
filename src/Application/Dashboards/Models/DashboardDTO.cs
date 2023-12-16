using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Dashboards.Models
{
    public sealed class DashboardDTO : EntityDTO
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public Guid workspaceId { get; set; }
    }
}
