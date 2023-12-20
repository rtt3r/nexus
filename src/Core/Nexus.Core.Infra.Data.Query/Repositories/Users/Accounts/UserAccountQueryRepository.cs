using Nexus.Core.Model.Users;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;

public class UserAccountQueryRepository(IAsyncDocumentSession dbSession) : RavenQueryRepository<UserAccount>(dbSession), IUserAccountQueryRepository
{
}