using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.BusinessGroups.Aggregates;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Core.Infra.Data.Configurations.BusinessGroups;
using Nexus.Core.Infra.Data.Configurations.Persons;

namespace Nexus.Core.Infra.Data;

internal sealed class CoreDbContext(DbContextOptions<CoreDbContext> options)
    : DbContext(options)
{
    public DbSet<Person> Persons { get; set; } = default!;
    // public DbSet<NaturalPerson> NaturalPersons { get; set; } = default!;
    public DbSet<LegalEntity> LegalEntities { get; set; } = default!;
    public DbSet<Document> Documents { get; set; } = default!;
    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<Contact> Phones { get; set; } = default!;
    public DbSet<BusinessGroup> BusinessGroups { get; set; } = default!;
    public DbSet<Company> Companies { get; set; } = default!;
    public DbSet<UserCompany> UserCompanies { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        // modelBuilder.ApplyConfiguration(new NaturalPersonConfiguration());
        modelBuilder.ApplyConfiguration(new LegalEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());

        modelBuilder.ApplyConfiguration(new BusinessGroupConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new UserCompanyConfiguration());
    }
}
