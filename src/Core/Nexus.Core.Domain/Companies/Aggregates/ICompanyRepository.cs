using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Companies.Aggregates;

public interface ICompanyRepository : IRepository<Company>
{
    Task<Company?> GetByCnpjAsync(string name, CancellationToken cancellationToken);
}
