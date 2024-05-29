using Goal.Domain.Events;
using MassTransit;
using Nexus.Core.Application.Events.Customers;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerRemovedEventConsumer(
    ICustomerQueryRepository customerRepository,
    IEventStore eventStore,
    ILogger<CustomerRemovedEventConsumer> logger) : EventConsumer<CustomerRemovedEvent>(eventStore, logger)
{
    private readonly ICustomerQueryRepository customerRepository = customerRepository;

    protected override async Task HandleEvent(CustomerRemovedEvent @event, CancellationToken cancellationToken = default)
        => await customerRepository.RemoveAsync(@event.AggregateId, cancellationToken);

    public class ConsumerDefinition : ConsumerDefinition<CustomerRemovedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CustomerRemovedEventConsumer> consumerConfigurator, IRegistrationContext context)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
