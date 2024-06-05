using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;
using Nexus.Core.Domain.Users.Events;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserProfileUpdatedEventConsumer(
    IUserAccountQueryRepository userAccountRepository,
    IEventStore eventStore,
    IMediator mediator,
    ITypeAdapter typeAdapter,
    ILogger<UserProfileUpdatedEventConsumer> logger)
    : EventConsumer<UserProfileUpdatedEvent>(eventStore, mediator, typeAdapter, logger)
{
    private readonly IUserAccountQueryRepository userAccountRepository = userAccountRepository;

    protected override async Task HandleEvent(UserProfileUpdatedEvent @event, CancellationToken cancellationToken = default)
    {
        await userAccountRepository.StoreAsync(
            @event.AggregateId,
            typeAdapter.Adapt<UserAccount>(@event.UserAccount),
            cancellationToken);
    }
}
