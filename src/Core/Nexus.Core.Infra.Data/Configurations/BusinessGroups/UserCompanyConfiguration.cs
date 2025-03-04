using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.BusinessGroups.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.BusinessGroups;

public sealed class UserCompanyConfiguration : IEntityTypeConfiguration<UserCompany>
{
    public void Configure(EntityTypeBuilder<UserCompany> builder)
    {
        builder.ToTable("UserCompanies", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.CompanyId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.RoleInCompany)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();
    }
}