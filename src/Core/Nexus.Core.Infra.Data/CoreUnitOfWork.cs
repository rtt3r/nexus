using Goal.Infra.Data;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data;

internal sealed class CoreUnitOfWork(
    CoreDbContext context,
    IPersonRepository persons,
    INaturalPersonRepository naturalPersons,
    ILegalEntityRepository legalEntities,
    IDocumentTypeRepository documentTypes)
    : UnitOfWork(context), ICoreUnitOfWork
{
    public IPersonRepository Persons { get; } = persons;
    public INaturalPersonRepository NaturalPersons { get; } = naturalPersons;
    public ILegalEntityRepository LegalEntities { get; } = legalEntities;
    public IDocumentTypeRepository DocumentTypes { get; } = documentTypes;
}
