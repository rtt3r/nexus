using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Companies.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class CompanyRepository(CoreDbContext context)
    : Repository<Company>(context), ICompanyRepository
{
    public async Task<Company?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken)
    {
        return await Context
            .Set<Company>()
            .FirstOrDefaultAsync(
                c => c.Documents.Any(d => d.Document.Name == "Cnpj" && d.Value == cnpj),
                cancellationToken);
    }
}
