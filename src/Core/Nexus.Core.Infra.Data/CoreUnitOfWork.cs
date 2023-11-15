using Nexus.Core.Domain.Customers.Aggregates;
using Goal.Seedwork.Infra.Data;

namespace Nexus.Core.Infra.Data;

public sealed class CoreUnitOfWork : UnitOfWork, ICoreUnitOfWork
{
    public CoreUnitOfWork(
        CoreDbContext context,
        ICustomerRepository customerRepository)
        : base(context)
    {
        Customers = customerRepository;
    }

    public ICustomerRepository Customers { get; }
}
