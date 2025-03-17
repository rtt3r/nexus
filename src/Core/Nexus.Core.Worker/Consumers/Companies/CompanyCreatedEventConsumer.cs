using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using Nexus.Core.Domain.Companies.Aggregates;
using Nexus.Core.Domain.Companies.Events;
using Nexus.Core.Infra.Data;
using Nexus.Core.Infra.Data.Query.Repositories.Companies;

namespace Nexus.Core.Worker.Consumers.Companies;

public class CompanyCreatedEventConsumer(
    ICompanyQueryRepository customerQueryRepository,
    ICoreUnitOfWork uow,
    IEventStore eventStore,
    ITypeAdapter typeAdapter,
    ILogger<CompanyCreatedEventConsumer> logger)
    : EventConsumer<CompanyCreatedEvent>(eventStore, logger)
{
    private readonly ICompanyQueryRepository customerQueryRepository = customerQueryRepository;
    private readonly ICoreUnitOfWork uow = uow;

    protected override async Task HandleEvent(CompanyCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        Company? company = await uow.Companies.GetAsync(@event.AggregateId, cancellationToken);

        if (company is null)
        {
            return;
        }

        await customerQueryRepository.StoreAsync(
            @event.AggregateId,
            typeAdapter.Adapt<Model.Companies.Company>(company),
            cancellationToken);
    }
}
