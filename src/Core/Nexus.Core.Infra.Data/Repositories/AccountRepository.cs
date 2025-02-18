using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Accounts.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

internal sealed class AccountRepository(CoreDbContext context)
    : Repository<Account, string>(context), IAccountRepository
{
    public async Task<Account?> GetFromUserAsync(string userId, string accountId, CancellationToken cancellationToken = default)
    {
        return await Context
            .Set<Account>()
            .FirstOrDefaultAsync(
                p => p.UserId == userId && p.Id == accountId,
                cancellationToken);
    }

    public async Task<Account?> GetFromUserByName(string userId, string name, CancellationToken cancellationToken = default)
    {
        return await Context
            .Set<Account>()
            .FirstOrDefaultAsync(
                p => p.UserId == userId && p.Name == name,
                cancellationToken);
    }
}
