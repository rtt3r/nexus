using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Finance.Domain.Accounts.Aggregates;

namespace Nexus.Finance.Infra.Data.Repositories;

internal sealed class AccountRepository(FinanceDbContext context)
    : Repository<Account, string>(context), IAccountRepository
{
    public async Task<Account?> GetByUserAndName(string userId, string name, CancellationToken cancellationToken = default)
    {
        return await Context
            .Set<Account>()
            .FirstOrDefaultAsync(
                p => p.UserId.Equals(userId, StringComparison.CurrentCultureIgnoreCase)
                && p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken);
    }
}
