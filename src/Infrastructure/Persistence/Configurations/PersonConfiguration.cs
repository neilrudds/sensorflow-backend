using SensorFlow.Domain.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SensorFlow.Infrastructure.Persistence.Configurations;

/* Specific configuration for the Person Entity */

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        /*builder.Property(p => p.Id)
            .HasConversion(personId => personId.Value,
            value => new PersonId(value));*/

        builder.Property(p => p.Email)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder.Property(p => p.Phone)
            .HasColumnType("varchar(64)")
            .IsRequired();
    }
}