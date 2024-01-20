using Nexus.Core.Domain.Customers.Aggregates;
using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.Repositories;

public class CustomerRepository(CoreDbContext context) : Repository<Customer, string>(context), ICustomerRepository
{
    public async Task<Customer?> GetByEmail(string email)
    {
        return await Context
            .Set<Customer>()
            .FirstOrDefaultAsync(p => p.Email == email);
    }
}
