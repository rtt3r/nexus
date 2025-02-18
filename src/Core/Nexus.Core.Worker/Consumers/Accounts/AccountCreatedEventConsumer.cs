using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using Nexus.Core.Domain.Accounts.Aggregates;
using Nexus.Core.Domain.Accounts.Events;
using Nexus.Core.Infra.Data;
using Nexus.Core.Infra.Data.Query.Repositories.Accounts;

namespace Nexus.Core.Worker.Consumers.Accounts;

public class AccountCreatedEventConsumer(
    IAccountQueryRepository customerQueryRepository,
    ICoreUnitOfWork uow,
    IEventStore eventStore,
    ITypeAdapter typeAdapter,
    ILogger<AccountCreatedEventConsumer> logger)
    : EventConsumer<AccountCreatedEvent>(eventStore, logger)
{
    private readonly IAccountQueryRepository customerQueryRepository = customerQueryRepository;
    private readonly ICoreUnitOfWork uow = uow;

    protected override async Task HandleEvent(AccountCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        Account? account = await uow.Accounts.GetAsync(@event.AggregateId, cancellationToken);

        if (account is null)
        {
            return;
        }

        await customerQueryRepository.StoreAsync(
            @event.AggregateId,
            typeAdapter.Adapt<Model.Accounts.Account>(account),
            cancellationToken);
    }
}
