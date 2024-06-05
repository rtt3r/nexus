using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;
using Nexus.Core.Domain.Customers.Events;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerRemovedEventConsumer(
    ICustomerQueryRepository customerRepository,
    IEventStore eventStore,
    IMediator mediator,
    ITypeAdapter typeAdapter,
    ILogger<CustomerRemovedEventConsumer> logger)
    : EventConsumer<CustomerRemovedEvent>(eventStore, mediator, typeAdapter, logger)
{
    private readonly ICustomerQueryRepository customerRepository = customerRepository;

    protected override async Task HandleEvent(CustomerRemovedEvent @event, CancellationToken cancellationToken = default)
        => await customerRepository.RemoveAsync(@event.AggregateId, cancellationToken);
}
