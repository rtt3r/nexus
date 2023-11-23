using Nexus.Core.Application.Events.Users;
using Goal.Seedwork.Domain.Events;
using MassTransit;
using Nexus.Core.Model.Users;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;

namespace Nexus.Core.Worker.Consumers.Users;

public class UserAccountCreatedEventConsumer : EventConsumer<UserAccountCreatedEvent>
{
    private readonly IUserAccountQueryRepository userAccountRepository;

    public UserAccountCreatedEventConsumer(
        IUserAccountQueryRepository userAccountRepository,
        IEventStore eventStore,
        ILogger<UserAccountCreatedEventConsumer> logger)
        : base(eventStore, logger)
    {
        this.userAccountRepository = userAccountRepository;
    }

    protected override async Task HandleEvent(UserAccountCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        await userAccountRepository.StoreAsync(
            @event.AggregateId,
            new UserAccount
            {
                Id = @event.AggregateId,
                Email = @event.Email,
                Username = @event.Username,
                Profile = new UserProfile
                {
                    Name = @event.Name
                }
            },
            cancellationToken);
    }

    public class ConsumerDefinition : ConsumerDefinition<UserAccountCreatedEventConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserAccountCreatedEventConsumer> consumerConfigurator)
            => consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}
