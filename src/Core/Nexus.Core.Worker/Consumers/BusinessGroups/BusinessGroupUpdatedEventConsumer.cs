using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using Nexus.Core.Domain.BusinessGroups.Aggregates;
using Nexus.Core.Domain.BusinessGroups.Events;
using Nexus.Core.Infra.Data;
using Nexus.Core.Infra.Data.Query.Repositories.BusinessGroups;

namespace Nexus.Core.Worker.Consumers.BusinessGroups;

public class BusinessGroupUpdatedEventConsumer(
    IBusinessGroupQueryRepository customerQueryRepository,
    ICoreUnitOfWork uow,
    IEventStore eventStore,
    ITypeAdapter typeAdapter,
    ILogger<BusinessGroupUpdatedEventConsumer> logger)
    : EventConsumer<BusinessGroupUpdatedEvent>(eventStore, logger)
{
    private readonly IBusinessGroupQueryRepository customerQueryRepository = customerQueryRepository;
    private readonly ICoreUnitOfWork uow = uow;

    protected override async Task HandleEvent(BusinessGroupUpdatedEvent @event, CancellationToken cancellationToken = default)
    {
        BusinessGroup? businessgroup = await uow.BusinessGroups.GetAsync(@event.AggregateId, cancellationToken);

        if (businessgroup is null)
        {
            return;
        }

        await customerQueryRepository.StoreAsync(
            @event.AggregateId,
            typeAdapter.Adapt<Model.BusinessGroups.BusinessGroup>(businessgroup),
            cancellationToken);
    }
}
