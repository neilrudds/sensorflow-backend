using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Dashboards.Models
{
    // DashboardDTO inherits the EntityDTO object
    public sealed class DashboardDTO : EntityDTO
    {
        public string id { get; set; }
        public string? name { get; set; }
        public string workspaceId { get; set; }
        public string gridWidgets { get; set; }
        public string gridLayout { get; set; }
    }
}
