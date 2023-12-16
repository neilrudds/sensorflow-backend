using SensorFlow.Domain.Entities.Dashboards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

/* Specific configuration for the Workspace Entity */
namespace SensorFlow.Infrastructure.Configurations
{
    internal class DashboardConfiguration : IEntityTypeConfiguration<Dashboard>
    {
        public void Configure(EntityTypeBuilder<Dashboard> builder)
        {
            builder.ToTable("Dashboards", "Sflow");
            builder.HasKey(p => p.Id);

            //builder.Property(p => p.Id)
            //    .HasConversion(workspaceId => workspaceId.Value,
            //    value => new WorkspaceId(value));

            builder.Property(p => p.Name)
                .HasColumnType("varchar(64)")
            .IsRequired();

            builder.HasOne(p => p.Workspace)
                .WithMany(p => p.Dashboards)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}