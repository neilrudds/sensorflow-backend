using SensorFlow.Domain.Entities.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/* Specific configuration for the Workspace Entity */
namespace SensorFlow.Infrastructure.Configurations
{
    internal class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices", "Sflow");
            builder.HasKey(p => p.Id);

            //builder.Property(p => p.Id)
            //    .HasConversion(workspaceId => workspaceId.Value,
            //    value => new WorkspaceId(value));

            builder.Property(p => p.Name)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder.Property(p => p.Fields)
                .HasMaxLength(int.MaxValue);

            builder.HasOne(p => p.Workspace)
               .WithMany(p => p.Devices)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

            builder.HasOne(p => p.Gateway)
                .WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}