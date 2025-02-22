using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public interface ILegalEntityRepository : IRepository<LegalEntity>
{
    Task<LegalEntity?> GetByCnpj(string cnpj, CancellationToken cancellationToken);
}
