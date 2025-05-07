using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class LegalEntityRepository(CoreDbContext context)
    : Repository<LegalEntity, string>(context), ILegalEntityRepository
{
    public async Task<LegalEntity?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken)
    {
        return await Context.Set<LegalEntity>()
            .FirstOrDefaultAsync(
                c => c.Documents.Any(d => d.Document.Name == "Cnpj" && d.Value == cnpj),
                cancellationToken);
    }
}
