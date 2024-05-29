using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public interface IUserProfileRepository : IRepository<UserProfile, string>
{
}
