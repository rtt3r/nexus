using Nexus.Core.Model.Users;
using Goal.Seedwork.Infra.Data.Query;

namespace Nexus.Core.Infra.Data.Query.Repositories.Users;

public interface IUserQueryRepository : IQueryRepository<User, string>
{
}
