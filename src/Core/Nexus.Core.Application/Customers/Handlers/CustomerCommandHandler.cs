using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Application.Customers.Commands;
using Nexus.Core.Application.Customers.Validators;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.Customers.Events;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Crosscutting.Extensions;
using OneOf;
using OneOf.Types;
using CustomerModel = Nexus.Core.Model.Customers.Customer;

namespace Nexus.Core.Application.Customers.Handlers;

internal class CustomerCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, typeAdapter),
    ICommandHandler<RegisterCustomerCommand, OneOf<CustomerModel, AppError>>,
    ICommandHandler<UpdateCustomerCommand, OneOf<None, AppError>>,
    ICommandHandler<RemoveCustomerCommand, OneOf<None, AppError>>
{
    private readonly IPublishEndpoint publishEndpoint = publishEndpoint;
    private readonly AppState appState = appState;

    public async Task<OneOf<CustomerModel, AppError>> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<RegisterCustomerCommandValidator, RegisterCustomerCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        Customer? customer = await uow.Customers.GetByEmail(command.Email!);

        if (customer is not null)
        {
            return new BusinessRuleError(Notifications.Customer.CUSTOMER_EMAIL_DUPLICATED);
        }

        customer = new Customer(command.Name!, command.Email!, command.Birthdate!.Value);

        await uow.Customers.AddAsync(customer, cancellationToken);

        await uow.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(
            new CustomerCreatedEvent(customer.Id, appState.User!.UserId),
            cancellationToken);

        return ProjectAs<CustomerModel>(customer);
    }

    public async Task<OneOf<None, AppError>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<UpdateCustomerCommandValidator, UpdateCustomerCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        Customer? customer = await uow.Customers.GetAsync(command.CustomerId!, cancellationToken);

        if (customer is null)
        {
            return new ResourceNotFoundError(Notifications.Customer.CUSTOMER_NOT_FOUND);
        }

        if (await uow.Customers.HasAnotherWithEmailAsync(command.CustomerId!, command.Email!))
        {
            return new BusinessRuleError(Notifications.Customer.CUSTOMER_EMAIL_DUPLICATED);
        }

        customer.UpdateName(command.Name!);
        customer.UpdateBirthdate(command.Birthdate!.Value);
        customer.UpdateEmail(command.Email!);

        uow.Customers.Update(customer);

        await uow.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(
            new CustomerUpdatedEvent(customer.Id, appState.User!.UserId),
            cancellationToken);

        return default(None);
    }

    public async Task<OneOf<None, AppError>> Handle(RemoveCustomerCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<RemoveCustomerCommandValidator, RemoveCustomerCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        Customer? customer = await uow.Customers.GetAsync(command.CustomerId!, cancellationToken);

        if (customer is null)
        {
            return new ResourceNotFoundError(Notifications.Customer.CUSTOMER_NOT_FOUND);
        }

        uow.Customers.Remove(customer);

        await uow.CommitAsync(cancellationToken);

        await publishEndpoint.Publish(
            new CustomerRemovedEvent(command.CustomerId!, appState.User!.UserId),
            cancellationToken);

        return default(None);
    }
}
