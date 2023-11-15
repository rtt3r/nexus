using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public interface IUsersRepository : IRepository<User, string>
{
}
