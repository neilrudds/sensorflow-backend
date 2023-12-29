using AutoMapper;
using SensorFlow.Application.Devices.Models;
using SensorFlow.Domain.Entities.Devices;

namespace SensorFlow.Application.Devices.MappingProfiles
{
    public sealed class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDTO>();
        }
    }
}