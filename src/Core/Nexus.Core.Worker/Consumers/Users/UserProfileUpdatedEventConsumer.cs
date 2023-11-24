using Nexus.Core.Application.Events.Users;
using Goal.Seedwork.Domain.Events;
using MassTransit;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserProfileUpdatedEventConsumer : EventConsumer<UserProfileUpdatedEvent>
{
    private readonly IUserAccountQueryRepository userAccountRepository;

    public UserProfileUpdatedEventConsumer(
        IUserAccountQueryRepository userAccountRepository,
        IEventStore eventStore,
        ILogger<UserProfileUpdatedEventConsumer> logger)
        : base(eventStore, logger)
    {
        this.userAccountRepository = userAccountRepository;
    }

    protected override async Task HandleEvent(UserProfileUpdatedEvent @event, CancellationToken cancellationToken = default)
    {
        await userAccountRepository.StoreAsync(
            @event.AggregateId,
            @event.UserAccount,
            cancellationToken);
    }

    public class ConsumerDefinition : ConsumerDefinition<UserProfileUpdatedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserProfileUpdatedEventConsumer> consumerConfigurator)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
