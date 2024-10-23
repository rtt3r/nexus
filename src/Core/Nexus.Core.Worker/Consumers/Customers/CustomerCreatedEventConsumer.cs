using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.Customers.Events;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using CustomerModels = Nexus.Core.Model.Customers;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerCreatedEventConsumer(
    ICustomerQueryRepository customerQueryRepository,
    ICustomerRepository customerRepository,
    IEventStore eventStore,
    IMediator mediator,
    ITypeAdapter typeAdapter,
    ILogger<CustomerCreatedEventConsumer> logger)
    : EventConsumer<CustomerCreatedEvent>(eventStore, mediator, typeAdapter, logger)
{
    private readonly ICustomerQueryRepository customerQueryRepository = customerQueryRepository;
    private readonly ICustomerRepository customerRepository = customerRepository;

    protected override async Task HandleEvent(CustomerCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        Customer? customer = await customerRepository.LoadAsync(@event.AggregateId, cancellationToken);

        if (customer is null)
        {
            return;
        }

        await customerQueryRepository.StoreAsync(
            @event.AggregateId,
            typeAdapter.Adapt<CustomerModels.Customer>(customer),
            cancellationToken);
    }
}
