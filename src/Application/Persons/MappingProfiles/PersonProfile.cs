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
                .ForMember(dest => dest.AddedTime, e => e.MapFrom(src => src.AddedTime))
                .ForMember(dest => dest.LastModified, e => e.MapFrom(src => src.LastModified));
        }
    }
}
