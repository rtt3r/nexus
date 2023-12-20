using Nexus.Core.Application.Events.Users;
using Goal.Seedwork.Domain.Events;
using MassTransit;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserAccountCreatedEventConsumer(
    IUserAccountQueryRepository userAccountRepository,
    IEventStore eventStore,
    ILogger<UserAccountCreatedEventConsumer> logger) : EventConsumer<UserAccountCreatedEvent>(eventStore, logger)
{
    private readonly IUserAccountQueryRepository userAccountRepository = userAccountRepository;

    protected override async Task HandleEvent(UserAccountCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        await userAccountRepository.StoreAsync(
            @event.AggregateId,
            @event.UserAccount,
            cancellationToken);
    }

    public class ConsumerDefinition : ConsumerDefinition<UserAccountCreatedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserAccountCreatedEventConsumer> consumerConfigurator, IRegistrationContext context)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
