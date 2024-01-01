using AutoMapper;
using SensorFlow.Application.Tenants.Models;
using SensorFlow.Domain.Entities.Tenants;

namespace SensorFlow.Application.Tenants.MappingProfiles
{
    public sealed class TenantProfile : Profile
    {
        public TenantProfile() 
        {
            CreateMap<Tenant, TenantDTO>();
        }
    }
}
