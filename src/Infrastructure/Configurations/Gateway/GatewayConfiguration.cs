using SensorFlow.Domain.Entities.Gateways;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SensorFlow.Infrastructure.Configurations
{
    internal class GatewayConfiguration : IEntityTypeConfiguration<Gateway>
    {
        public void Configure(EntityTypeBuilder<Gateway> builder)
        {
            builder.ToTable("Gateways", "Sflow");
            builder.HasKey(p => p.Id);

            //builder.Property(p => p.Id)
            //    .HasConversion(workspaceId => workspaceId.Value,
            //    value => new WorkspaceId(value));

            builder.Property(p => p.Name)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder.HasOne(p => p.Workspace)
               .WithMany(p => p.Gateways)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

            builder.HasMany(p => p.Devices)
                .WithOne(p => p.Gateway)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
