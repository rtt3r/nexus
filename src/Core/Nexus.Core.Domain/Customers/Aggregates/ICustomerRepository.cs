using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Customers.Aggregates;

public interface ICustomerRepository : IRepository<Customer, string>
{
    Task<Customer> GetByEmail(string email);
}
