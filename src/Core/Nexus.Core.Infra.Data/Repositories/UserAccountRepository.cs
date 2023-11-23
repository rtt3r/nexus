using Goal.Seedwork.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class UserAccountRepository : Repository<UserAccount, string>, IUserAccountRepository
{
    public UserAccountRepository(CoreDbContext context)
        : base(context)
    {
    }
}
