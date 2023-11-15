using Goal.Seedwork.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class UsersRepository : Repository<User, string>, IUsersRepository
{
    public UsersRepository(CoreDbContext context)
        : base(context)
    {
    }
}
