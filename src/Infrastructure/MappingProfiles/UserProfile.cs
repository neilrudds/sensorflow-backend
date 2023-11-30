using AutoMapper;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Infrastructure.Models.Identity;

namespace SensorFlow.Infrastructure.MappingProfiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ApplicationUser>(MemberList.Source)
                .ForSourceMember(src => src.Roles, opt => opt.DoNotValidate());

            //CreateMap<ApplicationUser, User>();
        }
    }
}