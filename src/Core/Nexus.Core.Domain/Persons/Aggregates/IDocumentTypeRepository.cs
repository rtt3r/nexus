using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public interface IDocumentTypeRepository : IRepository<DocumentType>
{
    Task<DocumentType?> GetByName(string name, CancellationToken cancellationToken);
}
