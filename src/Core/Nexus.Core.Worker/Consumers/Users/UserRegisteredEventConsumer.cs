using Nexus.Core.Application.Events.Users;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
using Nexus.Core.Model.Users;
using Goal.Seedwork.Domain.Events;
using MassTransit;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserRegisteredEventConsumer : EventConsumer<UserRegisteredEvent>
{
    private readonly IUserQueryRepository userRepository;

    public UserRegisteredEventConsumer(
        IUserQueryRepository userRepository,
        IEventStore eventStore,
        ILogger<UserRegisteredEventConsumer> logger)
        : base(eventStore, logger)
    {
        this.userRepository = userRepository;
    }

    protected override async Task HandleEvent(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
    {
        await userRepository.StoreAsync(
            @event.AggregateId,
            new User
            {
                Id = @event.AggregateId,
                Name = @event.Name,
                Email = @event.Email,
            },
            cancellationToken);
    }

    public class ConsumerDefinition : ConsumerDefinition<UserRegisteredEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserRegisteredEventConsumer> consumerConfigurator)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
