using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;
using Nexus.Core.Domain.Users.Events;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserAccountCreatedEventConsumer(
    IUserAccountQueryRepository userAccountRepository,
    IEventStore eventStore,
    IMediator mediator,
    ITypeAdapter typeAdapter,
    ILogger<UserAccountCreatedEventConsumer> logger)
    : EventConsumer<UserRegisteredEvent>(eventStore, mediator, typeAdapter, logger)
{
    private readonly IUserAccountQueryRepository userAccountRepository = userAccountRepository;

    protected override async Task HandleEvent(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
    {
        User user = typeAdapter.Adapt<User>(@event);

        await userAccountRepository.StoreAsync(
            @event.AggregateId,
            user,
            cancellationToken);
    }
}
