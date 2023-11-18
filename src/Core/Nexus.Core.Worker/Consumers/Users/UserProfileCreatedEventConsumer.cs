using Nexus.Core.Application.Events.Users;
using Goal.Seedwork.Domain.Events;
using MassTransit;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserProfileCreatedEventConsumer : EventConsumer<UserProfileCreatedEvent>
{
    private readonly IUserProfileQueryRepository userProfileRepository;

    public UserProfileCreatedEventConsumer(
        IUserProfileQueryRepository userProfileRepository,
        IEventStore eventStore,
        ILogger<UserProfileCreatedEventConsumer> logger)
        : base(eventStore, logger)
    {
        this.userProfileRepository = userProfileRepository;
    }

    protected override async Task HandleEvent(UserProfileCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        await userProfileRepository.StoreAsync(
            @event.AggregateId,
            new UserProfile
            {
                Id = @event.AggregateId
            },
            cancellationToken);
    }

    public class ConsumerDefinition : ConsumerDefinition<UserProfileCreatedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserProfileCreatedEventConsumer> consumerConfigurator)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
