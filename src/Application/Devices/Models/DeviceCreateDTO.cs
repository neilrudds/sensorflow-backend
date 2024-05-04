namespace SensorFlow.Application.Devices.Models
{
    public sealed class DeviceCreateDTO
    {
        public string id { get; set; }

        public string name { get; set; }

        public string location { get; set; }

        public string workspaceId { get; set; }

        public string gatewayId { get; set; }

        public string fields { get; set; }
    }
}
