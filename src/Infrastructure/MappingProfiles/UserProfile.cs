using AutoMapper;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.Enumerations;
using SensorFlow.Infrastructure.Extensions;
using SensorFlow.Infrastructure.Models.Identity;

namespace SensorFlow.Infrastructure.MappingProfiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUserRole, RoleEnum>()
                .ConvertUsing(r => r.Role.Name.ToEnum<RoleEnum>());

            CreateMap<User, ApplicationUser>()
                .ForMember(target => target.Roles, opt => opt.Ignore());

            CreateMap<ApplicationUser, User>();
        }
    }
}