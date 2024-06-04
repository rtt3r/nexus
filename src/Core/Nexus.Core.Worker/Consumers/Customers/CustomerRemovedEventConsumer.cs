using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Application.Events.Customers;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerRemovedEventConsumer(
    ICustomerQueryRepository customerRepository,
    IEventStore eventStore,
    IMediator mediator,
    ILogger<CustomerRemovedEventConsumer> logger)
    : EventConsumer<CustomerRemovedEvent>(eventStore, mediator, logger)
{
    private readonly ICustomerQueryRepository customerRepository = customerRepository;

    protected override async Task HandleEvent(CustomerRemovedEvent @event, CancellationToken cancellationToken = default)
        => await customerRepository.RemoveAsync(@event.AggregateId, cancellationToken);
}
