using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Hcm.Domain.Persons.Aggregates;

namespace Nexus.Hcm.Infra.Data.Configurations;

public sealed class ContactConfiguration : IEntityTypeConfiguration<PersonContact>
{
    public void Configure(EntityTypeBuilder<PersonContact> builder)
    {
        builder.ToTable("PersonContacts", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.PersonId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Type)
            .HasMaxLength(32)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(128);

        builder.Property(p => p.Email)
            .HasMaxLength(128);

        builder.Property(p => p.LandlinePhone)
            .HasMaxLength(32);

        builder.Property(p => p.MobilePhone)
            .HasMaxLength(32);

        builder.Property(p => p.Whatsapp)
            .HasMaxLength(32);
    }
}
