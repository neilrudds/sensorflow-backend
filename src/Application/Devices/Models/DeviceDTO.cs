using SensorFlow.Application.Common.Models;

namespace SensorFlow.Application.Devices.Models
{
    public sealed class DeviceDTO : EntityDTO
    {
        public string Id { get; set; }
        public string? name { get; set; }

        public string? location { get; set; }

        public string? fields { get; set; }
    }
}
