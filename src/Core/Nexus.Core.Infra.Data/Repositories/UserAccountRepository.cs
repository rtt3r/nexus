using Goal.Seedwork.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class UserAccountRepository(CoreDbContext context) : Repository<UserAccount, string>(context), IUserAccountRepository
{
}
