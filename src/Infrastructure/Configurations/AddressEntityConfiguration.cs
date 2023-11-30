using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Infrastructure.Models;

namespace SensorFlow.Infrastructure.Configurations
{
    internal class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("UserAddresses");
            builder.Property(a => a.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}
