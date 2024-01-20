using Nexus.Core.Application.Events.Users;
using Goal.Domain.Events;
using MassTransit;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserProfileUpdatedEventConsumer(
    IUserAccountQueryRepository userAccountRepository,
    IEventStore eventStore,
    ILogger<UserProfileUpdatedEventConsumer> logger) : EventConsumer<UserProfileUpdatedEvent>(eventStore, logger)
{
    private readonly IUserAccountQueryRepository userAccountRepository = userAccountRepository;

    protected override async Task HandleEvent(UserProfileUpdatedEvent @event, CancellationToken cancellationToken = default)
    {
        await userAccountRepository.StoreAsync(
            @event.AggregateId,
            @event.UserAccount,
            cancellationToken);
    }

    public class ConsumerDefinition : ConsumerDefinition<UserProfileUpdatedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserProfileUpdatedEventConsumer> consumerConfigurator, IRegistrationContext context)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
