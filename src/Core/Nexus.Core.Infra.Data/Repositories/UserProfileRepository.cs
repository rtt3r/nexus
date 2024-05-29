using Goal.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class UserProfileRepository(CoreDbContext context) : Repository<UserProfile, string>(context), IUserProfileRepository
{
}
