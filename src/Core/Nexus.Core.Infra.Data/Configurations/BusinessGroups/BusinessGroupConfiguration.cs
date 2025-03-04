using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.BusinessGroups.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.BusinessGroups;

public sealed class BusinessGroupConfiguration : IEntityTypeConfiguration<BusinessGroup>
{
    public void Configure(EntityTypeBuilder<BusinessGroup> builder)
    {
        builder.ToTable("BusinessGroups", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(128);

        builder.Property(p => p.Description)
            .HasMaxLength(256);

        builder.Property(p => p.TaxId)
            .HasMaxLength(32);

        builder.HasIndex(p => p.TaxId)
            .IsUnique();

        builder.HasMany(p => p.Companies)
            .WithOne(p => p.BusinessGroup)
            .HasForeignKey(p => p.BusinessGroupId);
    }
}
