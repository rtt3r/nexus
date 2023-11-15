using Nexus.Core.Domain.Customers.Aggregates;
using Goal.Seedwork.Domain;

namespace Nexus.Core.Infra.Data;

public interface ICoreUnitOfWork : IUnitOfWork
{
    ICustomerRepository Customers { get; }
}
