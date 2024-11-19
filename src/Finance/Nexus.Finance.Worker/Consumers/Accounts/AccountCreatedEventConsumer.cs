using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using Nexus.Finance.Domain.Accounts.Aggregates;
using Nexus.Finance.Domain.Accounts.Events;
using Nexus.Finance.Infra.Data;
using Nexus.Finance.Infra.Data.Query.Repositories.Accounts;

namespace Nexus.Finance.Worker.Consumers.Accounts;

public class AccountCreatedEventConsumer(
    IAccountQueryRepository customerQueryRepository,
    IFinanceUnitOfWork uow,
    IEventStore eventStore,
    ITypeAdapter typeAdapter,
    ILogger<AccountCreatedEventConsumer> logger)
    : EventConsumer<AccountCreatedEvent>(eventStore, logger)
{
    private readonly IAccountQueryRepository customerQueryRepository = customerQueryRepository;
    private readonly IFinanceUnitOfWork uow = uow;

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
