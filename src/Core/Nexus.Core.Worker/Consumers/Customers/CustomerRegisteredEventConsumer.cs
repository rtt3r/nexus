using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Application.Events.Customers;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using Nexus.Core.Model.Customers;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerRegisteredEventConsumer(
    ICustomerQueryRepository customerRepository,
    IEventStore eventStore,
    IMediator mediator,
    ILogger<CustomerRegisteredEventConsumer> logger)
    : EventConsumer<CustomerRegisteredEvent>(eventStore, mediator, logger)
{
    private readonly ICustomerQueryRepository customerRepository = customerRepository;

    protected override async Task HandleEvent(CustomerRegisteredEvent @event, CancellationToken cancellationToken = default)
    {
        await customerRepository.StoreAsync(
            @event.AggregateId,
            new Customer
            {
                CustomerId = @event.AggregateId,
                Name = @event.Name,
                Birthdate = @event.Birthdate,
                Email = @event.Email,
            },
            cancellationToken);
    }

    public class ConsumerDefinition : EventConsumerDefinition<CustomerRegisteredEventConsumer>
    {
    }
}
