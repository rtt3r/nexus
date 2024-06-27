using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.People.Aggregates;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Infra.Data.Configurations.Customers;
using Nexus.Core.Infra.Data.Configurations.People;
using Nexus.Core.Infra.Data.Configurations.Users;

namespace Nexus.Core.Infra.Data;

public abstract class CoreDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<PersonAddress> PersonAddresses { get; set; }
    public DbSet<PersonAddressType> PersonAddressTypes { get; set; }
    public DbSet<PersonContact> PersonContacts { get; set; }
    public DbSet<PersonContactType> PersonContactTypes { get; set; }
    public DbSet<PersonDocument> PersonDocument { get; set; }
    public DbSet<PersonDocumentType> PersonDocumentType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new NaturalPersonConfiguration());
        modelBuilder.ApplyConfiguration(new LegalPersonConfiguration());
        modelBuilder.ApplyConfiguration(new PersonAddressConfiguration());
        modelBuilder.ApplyConfiguration(new PersonAddressTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PersonContactConfiguration());
        modelBuilder.ApplyConfiguration(new PersonContactTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PersonDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new PersonDocumentTypeConfiguration());
    }
}
