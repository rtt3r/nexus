using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Companies.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Companies;

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies", "Core");

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.HeadquartersId)
            .HasMaxLength(64);

        builder.Property(p => p.CompanyType)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.HasMany(p => p.Branches)
            .WithOne(p => p.Headquarters)
            .HasForeignKey(p => p.HeadquartersId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Users)
            .WithOne(p => p.Company)
            .HasForeignKey(p => p.CompanyId);
    }
}
