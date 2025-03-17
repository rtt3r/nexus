using Goal.Infra.Data;
using Nexus.Core.Domain.Companies.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class CompanyRepository(CoreDbContext context)
    : Repository<Company>(context), ICompanyRepository
{
    public Task<Company?> GetByCnpjAsync(string name, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
