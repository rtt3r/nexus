using Goal.Infra.Data;
using Nexus.Core.Domain.Companies.Aggregates;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data;

internal sealed class CoreUnitOfWork(
    CoreDbContext context,
    INaturalPersonRepository naturalPersons,
    ILegalEntityRepository legalEntities,
    ICompanyRepository companies)
    : UnitOfWork(context), ICoreUnitOfWork
{
    public INaturalPersonRepository NaturalPersons { get; } = naturalPersons;
    public ILegalEntityRepository LegalEntities { get; } = legalEntities;
    public ICompanyRepository Companies { get; } = companies;
}
