using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SensorFlow.Domain.ValueObjects;

namespace SensorFlow.Infrastructure.Configurations.ValueObjects
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("UserAddresses");
            builder.HasKey(p => p.Id);
        }
    }
}
