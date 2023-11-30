using AutoMapper;
using SensorFlow.Domain.ValueObjects;
using SensorFlow.Infrastructure.Models;


namespace SensorFlow.Infrastructure.MappingProfiles
{
    internal class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<AddressEntity, Address>().ReverseMap();

        }
    }
}
