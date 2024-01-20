using Nexus.Core.Application.Events.Customers;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using Nexus.Core.Model.Customers;
using Goal.Domain.Events;
using MassTransit;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerRegisteredEventConsumer(
    ICustomerQueryRepository customerRepository,
    IEventStore eventStore,
    ILogger<CustomerRegisteredEventConsumer> logger) : EventConsumer<CustomerRegisteredEvent>(eventStore, logger)
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

    public class ConsumerDefinition : ConsumerDefinition<CustomerRegisteredEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CustomerRegisteredEventConsumer> consumerConfigurator, IRegistrationContext context)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
