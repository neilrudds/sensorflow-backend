using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Domain.Entities.Users;

namespace SensorFlow.Infrastructure.Configurations.Identity
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            builder.HasOne(ur => ur.User)
                .WithMany(r => r.Roles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
