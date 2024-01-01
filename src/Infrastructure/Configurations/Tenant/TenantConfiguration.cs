using SensorFlow.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/* Specific configuration for the Tenant Entity */
namespace SensorFlow.Infrastructure.Configurations
{
    internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants", "Sflow");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("varchar(64)")
                .IsRequired();

            builder.HasMany(p => p.Workspaces)
                .WithOne(p => p.Tenant)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(p => p.WorkspaceCount);

            builder.Ignore(p => p.UserCount);
        }
    }
}