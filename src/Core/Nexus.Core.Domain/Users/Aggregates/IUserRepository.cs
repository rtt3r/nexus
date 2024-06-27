using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public interface IUserRepository : IRepository<User, string>
{
}
