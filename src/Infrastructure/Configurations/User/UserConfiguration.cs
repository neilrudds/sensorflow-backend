using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Domain.Entities.Users;
using SensorFlow.Domain.Entities.Workspaces;

namespace SensorFlow.Infrastructure.Configurations.Identity
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Workspaces)
                .WithMany(u => u.Users)
                .UsingEntity<Dictionary<string, object>>(
                j => j.HasOne<Workspace>().WithMany().OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.NoAction),
                j => j.ToTable("WorkspaceUser", "Sflow"));
        }
    }
}