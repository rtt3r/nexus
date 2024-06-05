using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;
using Nexus.Core.Domain.Customers.Events;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using Nexus.Core.Model.Customers;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerUpdatedEventConsumer(
    ICustomerQueryRepository customerRepository,
    IEventStore eventStore,
    IMediator mediator,
    ITypeAdapter typeAdapter,
    ILogger<CustomerUpdatedEventConsumer> logger)
    : EventConsumer<CustomerUpdatedEvent>(eventStore, mediator, typeAdapter, logger)
{
    private readonly ICustomerQueryRepository customerRepository = customerRepository;

    protected override async Task HandleEvent(CustomerUpdatedEvent @event, CancellationToken cancellationToken = default)
    {
        Customer? customer = await customerRepository.LoadAsync(@event.AggregateId, cancellationToken);

        if (customer is null)
        {
            return;
        }

        customer.Name = @event.Name;
        customer.Birthdate = @event.Birthdate;
        customer.Email = @event.Email;

        await customerRepository.StoreAsync(
            @event.AggregateId,
            customer,
            cancellationToken);
    }
}
