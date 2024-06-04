using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Users;

public class UserAccountsConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("UserAccounts");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(p => p.Username)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasOne(p => p.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(e => e.Id);
    }
}