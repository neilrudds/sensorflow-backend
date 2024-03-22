namespace SensorFlow.Application.Workspaces.Models
{
    public sealed class TenantWorkspaceCreateDTO
    {
        public string name { get; set; }
        public string? tenantId { get; set; }
    }
}