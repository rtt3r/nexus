using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.People;

public sealed class PersonEmailConfiguration : IEntityTypeConfiguration<PersonEmail>
{
    public void Configure(EntityTypeBuilder<PersonEmail> builder)
    {
        builder.ToTable("PersonEmails");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.PersonId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.MailAddress)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(p => new { p.PersonId, p.MailAddress })
            .IsUnique();
    }
}