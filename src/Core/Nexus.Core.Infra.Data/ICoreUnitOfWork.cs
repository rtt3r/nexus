using Goal.Domain;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data;

public interface ICoreUnitOfWork : IUnitOfWork
{
    IPersonRepository Persons { get; }
    INaturalPersonRepository NaturalPersons { get; }
    ILegalEntityRepository LegalEntities { get; }
    IDocumentTypeRepository DocumentTypes { get; }
}
