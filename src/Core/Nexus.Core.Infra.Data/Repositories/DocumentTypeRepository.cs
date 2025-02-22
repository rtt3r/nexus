using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class DocumentTypeRepository(CoreDbContext context)
    : Repository<DocumentType, string>(context), IDocumentTypeRepository
{
    public async Task<DocumentType?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await Context.Set<DocumentType>()
            .FirstOrDefaultAsync(
                d => d.Name == name,
                cancellationToken);
    }
}