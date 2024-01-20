using Nexus.Core.Model.Customers;
using Goal.Infra.Data.Query;

namespace Nexus.Core.Infra.Data.Query.Repositories.Customers;

public interface ICustomerQueryRepository : IQueryRepository<Customer, string>
{
}
