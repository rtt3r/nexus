using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Customers.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class CustomerRepository(CoreDbContext context) : Repository<Customer, string>(context), ICustomerRepository
{
    public async Task<Customer?> GetByEmail(string email)
    {
        return await Context
            .Set<Customer>()
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<bool> HasAnotherWithEmailAsync(string id, string email)
    {
        return await Context
            .Set<Customer>()
            .AnyAsync(p => p.Id != id && p.Email == email);
    }

}
