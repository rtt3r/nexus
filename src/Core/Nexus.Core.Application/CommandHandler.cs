using FluentValidation;
using Goal.Application.Commands;
using Goal.Application.Extensions;
using Goal.Domain.Events;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Collections;
using MassTransit;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application;

internal abstract class CommandHandler(ICoreUnitOfWork uow, IPublishEndpoint publishEndpoint, ITypeAdapter typeAdapter)
{
    protected readonly ICoreUnitOfWork uow = uow;
    protected readonly IPublishEndpoint publishEndpoint = publishEndpoint;
    protected readonly ITypeAdapter typeAdapter = typeAdapter;

    protected static async Task<OneOf<None, InputValidationError>> ValidateCommandAsync<TValidator, TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TValidator : IValidator<TCommand>, new()
        where TCommand : ICommand
        => await ValidateCommandAsync(command, new TValidator(), cancellationToken);

    protected static async Task<OneOf<None, InputValidationError>> ValidateCommandAsync<TCommand>(TCommand command, IValidator<TCommand> validator, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        FluentValidation.Results.ValidationResult validationResult = await command
            .ValidateCommandAsync(validator, cancellationToken);

        return !validationResult.IsValid
            ? new InputValidationError(validationResult.Errors)
            : default(None);
    }

    protected TProjection ProjectAs<TProjection>(object source)
        where TProjection : class, new()
        => typeAdapter.Adapt<TProjection>(source);

    protected TProjection ProjectAs<TSource, TProjection>(TSource source)
        where TSource : class
        where TProjection : class, new()
        => typeAdapter.Adapt<TSource, TProjection>(source);

    protected ICollection<TProjection> ProjectAsCollection<TProjection>(IEnumerable<object> source)
        where TProjection : class, new()
        => typeAdapter.AdaptList<TProjection>(source);

    protected ICollection<TProjection> ProjectAsCollection<TSource, TProjection>(IEnumerable<TSource> source)
        where TSource : class
        where TProjection : class, new()
        => typeAdapter.AdaptList<TSource, TProjection>(source);

    protected IPagedList<TProjection> ProjectAsPagedCollection<TProjection>(IPagedList<object> source)
        where TProjection : class, new()
        => typeAdapter.AdaptPagedList<TProjection>(source);

    protected IPagedList<TProjection> ProjectAsPagedCollection<TSource, TProjection>(IPagedList<TSource> source)
        where TSource : class
        where TProjection : class, new()
        => typeAdapter.AdaptPagedList<TSource, TProjection>(source);

    protected async Task RaiseEvent<T>(T @event, CancellationToken cancellationToken)
        where T : class, IEvent
    {
        await publishEndpoint.Publish(
            @event,
            cancellationToken);
    }
}
