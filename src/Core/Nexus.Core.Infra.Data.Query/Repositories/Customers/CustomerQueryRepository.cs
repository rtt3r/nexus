using Nexus.Core.Model.Customers;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Core.Infra.Data.Query.Repositories.Customers;

internal class CustomerQueryRepository(IAsyncDocumentSession dbSession) : RavenQueryRepository<Customer>(dbSession), ICustomerQueryRepository
{
}
