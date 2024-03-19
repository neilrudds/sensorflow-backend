using AutoMapper;
using SensorFlow.Application.Gateways.Models;
using SensorFlow.Domain.Entities.Gateways;

namespace SensorFlow.Application.Gateways.MappingProfiles
{
    public sealed class GatewayProfile : Profile
    {
        public GatewayProfile()
        {
            CreateMap<Gateway, GatewayDTO>();
        }
    }
}