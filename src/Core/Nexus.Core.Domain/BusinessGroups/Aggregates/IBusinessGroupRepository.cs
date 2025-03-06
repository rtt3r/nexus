using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.BusinessGroups.Aggregates;

public interface IBusinessGroupRepository : IRepository<BusinessGroup>
{
    Task<BusinessGroup?> GetByName(string name, CancellationToken cancellationToken = default);
    Task<BusinessGroup?> GetByTaxId(string taxId, CancellationToken cancellationToken = default);
}
