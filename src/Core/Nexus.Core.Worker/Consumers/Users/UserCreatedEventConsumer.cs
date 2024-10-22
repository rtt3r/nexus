using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;
using Nexus.Core.Domain.Users.Events;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
using UserModels = Nexus.Core.Model.Users;
using UsersAggregates = Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserCreatedEventConsumer(
    IUserQueryRepository userQueryRepository,
    UsersAggregates.IUserRepository userRepository,
    IEventStore eventStore,
    IMediator mediator,
    ITypeAdapter typeAdapter,
    ILogger<UserCreatedEventConsumer> logger)
    : EventConsumer<UserRegisteredEvent>(eventStore, mediator, typeAdapter, logger)
{
    private readonly IUserQueryRepository userQueryRepository = userQueryRepository;
    private readonly UsersAggregates.IUserRepository userRepository = userRepository;

    protected override async Task HandleEvent(UserRegisteredEvent @event, CancellationToken cancellationToken = default)
    {
        UsersAggregates.User? userEntity = await userRepository.LoadAsync(@event.AggregateId, cancellationToken);

        if (userEntity is null)
        {
            return;
        }

        UserModels.User userModel = typeAdapter.Adapt<UserModels.User>(userEntity);

        await userQueryRepository.StoreAsync(
            @event.AggregateId,
            userModel,
            cancellationToken);
    }
}
