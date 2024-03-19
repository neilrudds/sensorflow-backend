using SensorFlow.Domain.Entities.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/* Specific configuration for the Workspace Entity */
namespace SensorFlow.Infrastructure.Configurations
{
    internal class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
    {
        public void Configure(EntityTypeBuilder<Workspace> builder)
        {
            builder.ToTable("Workspaces", "Sflow");
            builder.HasKey(p => p.Id);

            //builder.Property(p => p.Id)
            //    .HasConversion(workspaceId => workspaceId.Value,
            //    value => new WorkspaceId(value));

            builder.Property(p => p.Name)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder.HasMany(p => p.Dashboards)
                .WithOne(p => p.Workspace)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Devices)
                .WithOne(p => p.Workspace)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Gateways)
                .WithOne(p => p.Workspace)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(p => p.Users)
            //    .WithMany(p => p.Workspaces)
            //    .UsingEntity(p => p.ToTable("WorkspaceUser", "Sflow"));

            builder.Ignore(p => p.DeviceCount);

            builder.Ignore(p => p.UserCount);
        }
    }
}