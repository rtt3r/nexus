using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Finance.Application.Accounts.Commands;
using Nexus.Finance.Application.Accounts.Validators;
using Nexus.Finance.Domain.Accounts.Aggregates;
using Nexus.Finance.Domain.Accounts.Events;
using Nexus.Finance.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Crosscutting.Extensions;
using OneOf;
using OneOf.Types;
using AccountModel = Nexus.Finance.Model.Accounts.Account;

namespace Nexus.Finance.Application.Accounts.Handlers;

internal class AccountCommandHandler(
    IFinanceUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, typeAdapter),
    ICommandHandler<RegisterAccountCommand, OneOf<AccountModel, AppError>>,
    ICommandHandler<UpdateAccountCommand, OneOf<None, AppError>>
    //ICommandHandler<RemoveAccountCommand, OneOf<None, AppError>>
{
    private readonly IPublishEndpoint publishEndpoint = publishEndpoint;
    private readonly AppState appState = appState;

    public async Task<OneOf<AccountModel, AppError>> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<RegisterAccountCommandValidator, RegisterAccountCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        Account? account = await uow.Accounts.GetByName(
            command.Name,
            cancellationToken);

        if (account is not null)
        {
            return new BusinessRuleError(Notifications.Accounts.NAME_DUPLICATED);
        }

        FinancialInstitution? financialInstitution = await uow.FinancialInstitutions.GetAsync(
            command.FinancialInstitutionId,
            cancellationToken);

        if (financialInstitution is not null)
        {
            return new BusinessRuleError(Notifications.Accounts.FINANCIAL_INSTITUTION_NOT_FOUND);
        }

        account = Account.CreateAccount(
            appState.User!.UserId,
            command.Name,
            command.Description,
            command.Type,
            financialInstitution!,
            command.InitialBalance,
            command.Overdraft);

        await uow.Accounts.AddAsync(account, cancellationToken);

        await uow.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(
            new AccountCreatedEvent(account.Id, appState.User!.UserId),
            cancellationToken);

        return ProjectAs<AccountModel>(account);
    }

    public async Task<OneOf<None, AppError>> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<UpdateAccountCommandValidator, UpdateAccountCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        Account? account = await uow.Accounts.GetAsync(command.AccountId!, cancellationToken);

        if (account is null)
        {
            return new ResourceNotFoundError(Notifications.Accounts.NOT_FOUND);
        }

        FinancialInstitution? financialInstitution = await uow.FinancialInstitutions.GetAsync(
            command.FinancialInstitutionId,
            cancellationToken);

        if (financialInstitution is not null)
        {
            return new BusinessRuleError(Notifications.Accounts.FINANCIAL_INSTITUTION_NOT_FOUND);
        }

        account.SetName(command.Name);
        account.SetType(command.Type);
        account.SetFinancialInstitution(financialInstitution!);
        account.SetInitialBalance(command.InitialBalance);
        account.SetInitialOverdraft(command.Overdraft);

        if (!string.IsNullOrWhiteSpace(command.Description))
            account.SetDescription(command.Description);

        uow.Accounts.Update(account);

        await uow.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(
            new AccountUpdatedEvent(account.Id, appState.User!.UserId),
            cancellationToken);

        return default(None);
    }

    //public async Task<OneOf<None, AppError>> Handle(RemoveAccountCommand command, CancellationToken cancellationToken)
    //{
    //    OneOf<None, InputValidationError> validation = await ValidateCommandAsync<RemoveAccountCommandValidator, RemoveAccountCommand>(command, cancellationToken);

    //    if (validation.IsError())
    //    {
    //        return validation.GetError();
    //    }

    //    Account? account = await uow.Accounts.GetAsync(command.AccountId!, cancellationToken);

    //    if (account is null)
    //    {
    //        return new ResourceNotFoundError(Notifications.Account.ACCOUNT_NOT_FOUND);
    //    }

    //    uow.Accounts.Remove(account);

    //    await uow.CommitAsync(cancellationToken);

    //    await publishEndpoint.Publish(
    //        new AccountRemovedEvent(command.AccountId!, appState.User!.UserId),
    //        cancellationToken);

    //    return default(None);
    //}
}
