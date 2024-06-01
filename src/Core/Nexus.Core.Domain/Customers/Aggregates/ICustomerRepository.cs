using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Customers.Aggregates;

public interface ICustomerRepository : IRepository<Customer, string>
{
    Task<Customer?> GetByEmail(string email);
    Task<bool> HasAnotherWithEmailAsync(string id, string email);
}
