﻿using Microsoft.AspNetCore.Identity;

namespace SensorFlow.Domain.Entities.Users
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
