using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Application.Events.Users;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserAccountCreatedEventConsumer(
    IUserAccountQueryRepository userAccountRepository,
    IEventStore eventStore,
    IMediator mediator,
    ILogger<UserAccountCreatedEventConsumer> logger)
    : EventConsumer<UserAccountCreatedEvent>(eventStore, mediator, logger)
{
    private readonly IUserAccountQueryRepository userAccountRepository = userAccountRepository;

    protected override async Task HandleEvent(UserAccountCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        await userAccountRepository.StoreAsync(
            @event.AggregateId,
            @event.UserAccount,
            cancellationToken);
    }

    public class ConsumerDefinition : EventConsumerDefinition<UserAccountCreatedEventConsumer>
    {
    }
}
