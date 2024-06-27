using Goal.Infra.Data.Query;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;

public interface IUserAccountQueryRepository : IQueryRepository<User, string>
{
}