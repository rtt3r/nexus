using Nexus.Core.Model.Persons;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Core.Infra.Data.Query.Repositories.Persons;

internal class PersonQueryRepository(IAsyncDocumentSession dbSession) : RavenQueryRepository<NaturalPerson>(dbSession), IPersonQueryRepository
{
}
