namespace SensorFlow.Application.Gateways.Models
{
    public sealed class GatewayCreateDTO
    {
        public string name { get; set; }

        public string workspaceId { get; set; }

        public string host { get; set; }

        public int portNumber { get; set; }

        public string? username { get; set; }

        public string? password { get; set; }

        public string? clientId { get; set; }

        public bool sSLEnabled { get; set; }
    }
}
