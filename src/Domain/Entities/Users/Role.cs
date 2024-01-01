using Microsoft.AspNetCore.Identity;

namespace SensorFlow.Domain.Entities.Users
{
    public class Role : IdentityRole
    {
        // Navigation Properties
        public ICollection<UserRole> UserRoles { get; set; }
    }
}