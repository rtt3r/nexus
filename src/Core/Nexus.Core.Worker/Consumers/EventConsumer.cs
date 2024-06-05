using System.Diagnostics;
using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using MassTransit.Metadata;
using MediatR;

namespace Nexus.Core.Worker.Consumers;

public abstract class EventConsumer<TEvent>(
    IEventStore eventStore,
    IMediator mediator,
    ITypeAdapter typeAdapter,
    ILogger logger)
    : IConsumer<TEvent>
    where TEvent : class, IEvent
{
    protected readonly IEventStore eventStore = eventStore;
    protected readonly IMediator mediator = mediator;
    protected readonly ITypeAdapter typeAdapter = typeAdapter;
    protected readonly ILogger logger = logger;

    protected virtual string ConsumerName { get; } = TypeMetadataCache<TEvent>.ShortName;

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        var timer = Stopwatch.StartNew();

        logger.LogInformation("{InformationData} Received event.", ConsumerName);

        try
        {
            await HandleEvent(context.Message);
            eventStore.Save(context.Message);

            timer.Stop();
            logger.LogInformation("{InformationData}: Successfully consumed event.", ConsumerName);
            await context.NotifyConsumed(timer.Elapsed, ConsumerName);
        }
        catch (Exception ex)
        {
            timer.Stop();
            logger.LogError(ex, "{InformationData}: An error occurred while consuming an event.", ConsumerName);
            await context.NotifyFaulted(timer.Elapsed, ConsumerName, ex);
        }
    }

    protected abstract Task HandleEvent(TEvent @event, CancellationToken cancellationToken = default);
}
