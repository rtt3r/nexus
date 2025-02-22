using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class LegalEntityRepository(CoreDbContext context)
    : Repository<LegalEntity, string>(context), ILegalEntityRepository
{
    public async Task<LegalEntity?> GetByCnpj(string cnpj, CancellationToken cancellationToken)
    {
        return await Context.Set<LegalEntity>()
            .FirstOrDefaultAsync(
                p => p.Documents.Any(d => d.Type.Name == Domains.DocumentTypes.CNPJ && d.Number == cnpj),
                cancellationToken);
    }
}
