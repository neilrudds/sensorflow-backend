using AutoMapper;
using SensorFlow.Application.Dashboards.Models;
using SensorFlow.Domain.Entities.Dashboards;

namespace SensorFlow.Application.Dashboards.MappingProfiles
{
    public sealed class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            // AutoMapper, map Dashbaodrd Entity, to the DasboardDTO for the API Response
            CreateMap<Dashboard, DashboardDTO>();
        }
    }
}