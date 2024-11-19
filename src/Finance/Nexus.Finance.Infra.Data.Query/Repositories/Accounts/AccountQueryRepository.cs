using Nexus.Finance.Model.Accounts;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Finance.Infra.Data.Query.Repositories.Accounts;

internal class AccountQueryRepository(IAsyncDocumentSession dbSession) : RavenQueryRepository<Account>(dbSession), IAccountQueryRepository
{
}
