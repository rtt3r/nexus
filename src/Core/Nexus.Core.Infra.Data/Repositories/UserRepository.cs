using Goal.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class UserRepository(CoreDbContext context) : Repository<User, string>(context), IUserRepository
{
}
