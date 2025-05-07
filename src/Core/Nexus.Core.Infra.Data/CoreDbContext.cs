using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Companies.Aggregates;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Core.Infra.Data.Configurations.Companies;
using Nexus.Core.Infra.Data.Configurations.Persons;

namespace Nexus.Core.Infra.Data;

internal sealed class CoreDbContext(DbContextOptions<CoreDbContext> options)
    : DbContext(options)
{
    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<NaturalPerson> NaturalPersons { get; set; } = default!;
    public DbSet<LegalEntity> LegalEntities { get; set; } = default!;
    public DbSet<PersonDocument> PersonDocuments { get; set; } = default!;
    public DbSet<Document> Documents { get; set; } = default!;
    public DbSet<DocumentAttribute> DocumentAttributes { get; set; } = default!;
    public DbSet<PersonDocumentAttribute> PersonDocumentAttributes { get; set; } = default!;
    public DbSet<PersonAddress> PersonAddresses { get; set; } = default!;
    public DbSet<PersonContact> PersonContacts { get; set; } = default!;
    public DbSet<Company> Companies { get; set; } = default!;
    public DbSet<UserCompany> UserCompanies { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new NaturalPersonConfiguration());
        modelBuilder.ApplyConfiguration(new LegalEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new PersonDocumentAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new UserCompanyConfiguration());
    }
}
