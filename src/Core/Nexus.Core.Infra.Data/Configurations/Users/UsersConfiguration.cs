using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Users;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "Core");

        builder.Property(p => p.Username)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Avatar)
            .HasMaxLength(256);
    }
}