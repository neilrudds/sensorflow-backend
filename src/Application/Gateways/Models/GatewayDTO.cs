using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Gateways.Models
{
    public sealed class GatewayDTO : EntityDTO
    {
        public Guid id { get; set; }

        public string? name { get; set; }

        public string? host { get; set; }

        public int? portNumber { get; set; }

        public string? username { get; set; }

        public string? password { get; set; }

        public string? clientId { get; set; }

        public bool? sSLEnabled { get; set; }
    }
}
