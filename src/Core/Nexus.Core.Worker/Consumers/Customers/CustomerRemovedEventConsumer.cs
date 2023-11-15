using Nexus.Core.Application.Events.Customers;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using Goal.Seedwork.Domain.Events;
using MassTransit;

namespace Nexus.Core.Worker.Consumers.Customers;

public class CustomerRemovedEventConsumer : EventConsumer<CustomerRemovedEvent>
{
    private readonly ICustomerQueryRepository customerRepository;

    public CustomerRemovedEventConsumer(
        ICustomerQueryRepository customerRepository,
        IEventStore eventStore,
        ILogger<CustomerRemovedEventConsumer> logger)
        : base(eventStore, logger)
    {
        this.customerRepository = customerRepository;
    }

    protected override async Task HandleEvent(CustomerRemovedEvent @event, CancellationToken cancellationToken = default)
        => await customerRepository.RemoveAsync(@event.AggregateId, cancellationToken);

    public class ConsumerDefinition : ConsumerDefinition<CustomerRemovedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CustomerRemovedEventConsumer> consumerConfigurator)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
