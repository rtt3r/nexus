using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Core.Domain.Persons.Events;
using Nexus.Core.Infra.Data;
using Nexus.Core.Infra.Data.Query.Repositories.Persons;

namespace Nexus.Core.Worker.Consumers.Persons;

public class PersonCreatedEventConsumer(
    IPersonQueryRepository customerQueryRepository,
    ICoreUnitOfWork uow,
    IEventStore eventStore,
    ITypeAdapter typeAdapter,
    ILogger<PersonCreatedEventConsumer> logger)
    : EventConsumer<NaturalPersonCreatedEvent>(eventStore, logger)
{
    private readonly IPersonQueryRepository customerQueryRepository = customerQueryRepository;
    private readonly ICoreUnitOfWork uow = uow;

    protected override async Task HandleEvent(NaturalPersonCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        Person? person = await uow.Persons.GetAsync(@event.AggregateId, cancellationToken);

        if (person is null)
        {
            return;
        }

        await customerQueryRepository.StoreAsync(
            @event.AggregateId,
            typeAdapter.Adapt<Model.Persons.NaturalPerson>(person),
            cancellationToken);
    }
}
