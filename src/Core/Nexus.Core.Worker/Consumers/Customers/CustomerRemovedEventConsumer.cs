using Goal.Domain.Events;
using Nexus.Core.Domain.Customers.Events;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerRemovedEventConsumer(
    ICustomerQueryRepository customerRepository,
    IEventStore eventStore,
    ILogger<CustomerRemovedEventConsumer> logger)
    : EventConsumer<CustomerRemovedEvent>(eventStore, logger)
{
    private readonly ICustomerQueryRepository customerRepository = customerRepository;

    protected override async Task HandleEvent(CustomerRemovedEvent @event, CancellationToken cancellationToken = default)
        => await customerRepository.RemoveAsync(@event.AggregateId, cancellationToken);
}
