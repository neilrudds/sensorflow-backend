using AutoMapper;
using SensorFlow.Application.Persons.Models;
using SensorFlow.Domain.Entities.Persons;

namespace SensorFlow.Application.Persons.MappingProfiles
{
    public sealed class PersonProfile : Profile
    {
        public PersonProfile() 
        {
            CreateMap<Person, PersonDTO>()
                .ForMember(dest => dest.Id, e => e.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, e => e.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, e => e.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, e => e.MapFrom(src => src.Phone))
                .ForMember(dest => dest.CreatedTimestamp, e => e.MapFrom(src => src.CreatedTimestamp))
                .ForMember(dest => dest.LastModifiedTimestamp, e => e.MapFrom(src => src.LastModifiedTimestamp));
        }
    }
}
