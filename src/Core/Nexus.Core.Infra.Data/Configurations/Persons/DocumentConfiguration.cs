using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Persons;

public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("PersonDocuments", "Core");
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

        builder.Property(p => p.Number)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(p => new { p.PersonId, p.Type, p.Number })
            .IsUnique();
    }
}