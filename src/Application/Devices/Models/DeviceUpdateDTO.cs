namespace SensorFlow.Application.Devices.Models
{
    public sealed class DeviceUpdateDTO
    {
        public string? name {  get; set; }

        public string? location { get; set; }

        public string? gatewayId { get; set; }

        public string? fields { get; set; }
    }
}
