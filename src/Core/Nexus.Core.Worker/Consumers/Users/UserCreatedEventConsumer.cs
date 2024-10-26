using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Domain.Users.Events;
using Nexus.Core.Infra.Data;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
using UserModels = Nexus.Core.Model.Users;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserCreatedEventConsumer(
    IUserQueryRepository userQueryRepository,
    ICoreUnitOfWork uow,
    IEventStore eventStore,
    ITypeAdapter typeAdapter,
    ILogger<UserCreatedEventConsumer> logger)
    : EventConsumer<UserRegisteredEvent>(eventStore,  logger)
{
    private readonly IUserQueryRepository userQueryRepository = userQueryRepository;
    private readonly ICoreUnitOfWork uow = uow;

    protected override async Task HandleEvent(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
    {
        User? user = await uow.Users.GetAsync(@event.AggregateId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await userQueryRepository.StoreAsync(
            @event.AggregateId,
            typeAdapter.Adapt<UserModels.User>(user),
            cancellationToken);
    }
}
