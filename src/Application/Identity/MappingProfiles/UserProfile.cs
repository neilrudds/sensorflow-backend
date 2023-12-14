using AutoMapper;
using SensorFlow.Application.Identity.Models;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Application.Identity.MappingProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserDTO>();
                //.ForMember(dest => dest.userName, e => e.MapFrom(src => src.UserName))
                //.ForMember(dest => dest.firstName, e => e.MapFrom(src => src.FirstName))
                //.ForMember(dest => dest.lastName, e => e.MapFrom(src => src.LastName))
                //.ForMember(dest => dest.email, e => e.MapFrom(src => src.Email))
                //.ForMember(dest => dest.isActive, e => e.MapFrom(src => src.IsActive));
        }
    }
}