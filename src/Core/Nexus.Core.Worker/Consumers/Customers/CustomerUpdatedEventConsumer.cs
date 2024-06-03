using Goal.Domain.Events;
using MassTransit;
using Nexus.Core.Application.Events.Customers;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using Nexus.Core.Model.Customers;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerUpdatedEventConsumer(
    ICustomerQueryRepository customerRepository,
    IEventStore eventStore,
    ILogger<CustomerUpdatedEventConsumer> logger) : EventConsumer<CustomerUpdatedEvent>(eventStore, logger)
{
    private readonly ICustomerQueryRepository customerRepository = customerRepository;

    protected override async Task HandleEvent(CustomerUpdatedEvent @event, CancellationToken cancellationToken = default)
    {
        Customer? customer = await customerRepository.LoadAsync(@event.AggregateId, cancellationToken);

        if (customer is null)
            return;

        customer.Name = @event.Name;
        customer.Birthdate = @event.Birthdate;
        customer.Email = @event.Email;

        await customerRepository.StoreAsync(
            @event.AggregateId,
            customer,
            cancellationToken);
    }

    public class ConsumerDefinition : ConsumerDefinition<CustomerUpdatedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CustomerUpdatedEventConsumer> consumerConfigurator, IRegistrationContext context)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
