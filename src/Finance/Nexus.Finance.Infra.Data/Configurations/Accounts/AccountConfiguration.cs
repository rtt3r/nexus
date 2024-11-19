using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Finance.Domain.Accounts.Aggregates;

namespace Nexus.Finance.Infra.Data.Configurations.Accounts;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts", "Finance");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(256);

        builder.Property(p => p.FinancialInstitutionId)
            .HasMaxLength(36)
            .IsRequired();

        builder.HasIndex(p => new { p.UserId, p.Name })
            .IsUnique();
    }
}

internal sealed class FinancialInstitutionConfiguration : IEntityTypeConfiguration<FinancialInstitution>
{
    public void Configure(EntityTypeBuilder<FinancialInstitution> builder)
    {
        builder.ToTable("FinancialInstitutions", "Finance");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(256);

        builder.Property(p => p.Icon)
            .HasMaxLength(16);

        builder.HasMany(p => p.Accounts)
            .WithOne(p => p.FinancialInstitution)
            .HasForeignKey(p => p.FinancialInstitutionId);
    }
}