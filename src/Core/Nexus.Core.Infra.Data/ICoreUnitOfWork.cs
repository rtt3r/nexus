using Goal.Domain;
using Nexus.Core.Domain.Companies.Aggregates;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data;

public interface ICoreUnitOfWork : IUnitOfWork
{
    INaturalPersonRepository NaturalPersons { get; }
    ILegalEntityRepository LegalEntities { get; }
    ICompanyRepository Companies { get; }
}
