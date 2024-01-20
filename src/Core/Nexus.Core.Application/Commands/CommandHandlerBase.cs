using FluentValidation;
using FluentValidation.Results;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Constants;
using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Notifications;
using MassTransit;
using Goal.Application.Extensions;
using Goal.Infra.Crosscutting.Collections;

namespace Nexus.Core.Application.Commands;

public abstract class CommandHandlerBase
{
    protected readonly ICoreUnitOfWork uow;
    protected readonly IPublishEndpoint publishEndpoint;
    protected readonly IDefaultNotificationHandler notificationHandler;
    protected readonly ITypeAdapter typeAdapter;
    protected readonly AppState appState;

    protected CommandHandlerBase(
        ICoreUnitOfWork uow,
        IPublishEndpoint publishEndpoint,
        IDefaultNotificationHandler notificationHandler,
        ITypeAdapter typeAdapter,
        AppState appState)
    {
        this.uow = uow;
        this.publishEndpoint = publishEndpoint;
        this.notificationHandler = notificationHandler;
        this.typeAdapter = typeAdapter;
        this.appState = appState;
    }

    protected async Task<bool> ValidateCommandAsync<TValidator, TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TValidator : IValidator<TCommand>, new()
        where TCommand : ICommand
    {
        return await ValidateCommandAsync(command, new TValidator(), cancellationToken);
    }

    protected async Task<bool> ValidateCommandAsync<TCommand>(TCommand command, IValidator<TCommand> validator, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        FluentValidation.Results.ValidationResult validationResult = await command.ValidateCommandAsync(
            validator,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            foreach (ValidationFailure error in validationResult.Errors)
            {
                await HandleInputValidationAsync(
                    error.ErrorCode,
                    error.ErrorMessage,
                    error.PropertyName,
                    cancellationToken);
            }

            return false;
        }

        return true;
    }

    protected virtual async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        if (await uow.SaveAsync(cancellationToken))
        {
            return true;
        }

        await HandleInternalErrorAsync(
            nameof(ApplicationConstants.Messages.SAVING_DATA_FAILURE),
            ApplicationConstants.Messages.SAVING_DATA_FAILURE,
            cancellationToken);

        return false;
    }

    protected async Task HandleDomainViolationAsync(string code, string message, CancellationToken cancellationToken)
    {
        await HandleAsync(
            Notification.DomainViolation(code, message),
            cancellationToken);
    }

    protected async Task HandleInputValidationAsync(string code, string message, string paramName, CancellationToken cancellationToken)
    {
        await HandleAsync(
            Notification.InputValidation(code, message, paramName),
            cancellationToken);
    }

    protected async Task HandleResourceNotFoundAsync(string code, string message, CancellationToken cancellationToken)
    {
        await HandleAsync(
            Notification.ResourceNotFound(code, message),
            cancellationToken);
    }

    protected async Task HandleInternalErrorAsync(string code, string message, CancellationToken cancellationToken)
    {
        await HandleAsync(
            Notification.InternalError(code, message),
            cancellationToken);
    }

    protected async Task HandleExternalErrorAsync(string code, string message, CancellationToken cancellationToken)
    {
        await HandleAsync(
            Notification.ExternalError(code, message),
            cancellationToken);
    }

    protected async Task HandleInformationAsync(string code, string message, CancellationToken cancellationToken)
    {
        await HandleAsync(
            Notification.Information(code, message),
            cancellationToken);
    }

    private async Task HandleAsync(Notification notification, CancellationToken cancellationToken)
    {
        await notificationHandler.HandleAsync(
            notification,
            cancellationToken);
    }

    protected TProjection ProjectAs<TProjection>(object source)
        where TProjection : class, new()
    {
        return typeAdapter.Adapt<TProjection>(source);
    }

    protected TProjection ProjectAs<TSource, TProjection>(TSource source)
        where TSource : class
        where TProjection : class, new()
    {
        return typeAdapter.Adapt<TSource, TProjection>(source);
    }

    protected ICollection<TProjection> ProjectAsCollection<TProjection>(IEnumerable<object> source)
        where TProjection : class, new()
    {
        return typeAdapter.AdaptList<TProjection>(source);
    }

    protected ICollection<TProjection> ProjectAsCollection<TSource, TProjection>(IEnumerable<TSource> source)
        where TSource : class
        where TProjection : class, new()
    {
        return typeAdapter.AdaptList<TSource, TProjection>(source);
    }

    protected IPagedList<TProjection> ProjectAsPagedCollection<TProjection>(IPagedList<object> source)
        where TProjection : class, new()
    {
        return typeAdapter.AdaptPagedList<TProjection>(source);
    }

    protected IPagedList<TProjection> ProjectAsPagedCollection<TSource, TProjection>(IPagedList<TSource> source)
        where TSource : class
        where TProjection : class, new()
    {
        return typeAdapter.AdaptPagedList<TSource, TProjection>(source);
    }
}
