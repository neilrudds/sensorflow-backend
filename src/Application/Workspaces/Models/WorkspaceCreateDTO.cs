namespace SensorFlow.Application.Workspaces.Models
{
    public sealed class WorkspaceCreateDTO
    {
        public string name { get; set; }
        public string? tenantId { get; set; }
        public string userName { get; set; }
    }
}