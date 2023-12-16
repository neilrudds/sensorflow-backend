namespace SensorFlow.Application.Dashboards.Models
{
    public sealed class DashboardCreateDTO
    {
        public string name { get; set; }
        public Guid workspaceId { get; set; }
    }
}
