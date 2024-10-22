using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Domain.Users.Events;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
using UserModels = Nexus.Core.Model.Users;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserCreatedEventConsumer(
    IUserQueryRepository userQueryRepository,
    IUserRepository userRepository,
    IEventStore eventStore,
    IMediator mediator,
    ITypeAdapter typeAdapter,
    ILogger<UserCreatedEventConsumer> logger)
    : EventConsumer<UserRegisteredEvent>(eventStore, mediator, typeAdapter, logger)
{
    private readonly IUserQueryRepository userQueryRepository = userQueryRepository;
    private readonly IUserRepository userRepository = userRepository;

    protected override async Task HandleEvent(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
    {
        User? user = await userRepository.LoadAsync(@event.AggregateId, cancellationToken);

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
