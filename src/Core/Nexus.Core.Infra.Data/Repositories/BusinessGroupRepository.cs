using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.BusinessGroups.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class BusinessGroupRepository(CoreDbContext context)
    : Repository<BusinessGroup>(context), IBusinessGroupRepository
{
    public async Task<BusinessGroup?> GetByName(string name, CancellationToken cancellationToken = default)
    {
        string normalizedName = name.Trim().ToLower();

        return await Context.Set<BusinessGroup>()
            .FirstOrDefaultAsync(
                p => p.Name.Trim().ToLower() == normalizedName,
                cancellationToken);
    }

    public async Task<BusinessGroup?> GetByTaxId(string taxId, CancellationToken cancellationToken = default)
    {
        string normalizedTaxId = taxId.Trim().ToLower();

        return await Context.Set<BusinessGroup>()
            .FirstOrDefaultAsync(
                p => p.TaxId != null && p.TaxId.Trim().ToLower() == normalizedTaxId,
                cancellationToken);
    }
}
