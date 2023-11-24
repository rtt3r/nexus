using Goal.Seedwork.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class UserAccountRepository : Repository<UserAccount, string>, IUserAccountRepository
{
    public UserAccountRepository(CoreDbContext context)
        : base(context)
    {
    }

    public override async Task<UserAccount> LoadAsync(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await Context
            .Set<UserAccount>()
            .Include(p => p.Profile)
            .FirstOrDefaultAsync(acc => acc.Id == key, cancellationToken);
    }
}
