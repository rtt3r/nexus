using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Notifications;
using MassTransit;
using Nexus.Core.Application.Commands.Customers.Validators;
using Nexus.Core.Application.Events.Customers;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Constants;
using CustomerModel = Nexus.Core.Model.Customers.Customer;

namespace Nexus.Core.Application.Commands.Customers;

public class CustomerCommandHandler(
    ICoreUnitOfWork uow,
    IPublishEndpoint publishEndpoint,
    IDefaultNotificationHandler notificationHandler,
    ITypeAdapter typeAdapter,
    AppState appState)
    : CommandHandlerBase(uow, publishEndpoint, notificationHandler, typeAdapter, appState),
    ICommandHandler<RegisterCustomerCommand, ICommandResult<CustomerModel>>,
    ICommandHandler<UpdateCustomerCommand, ICommandResult>,
    ICommandHandler<RemoveCustomerCommand, ICommandResult>
{
    public async Task<ICommandResult<CustomerModel>> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        if (!await ValidateCommandAsync<RegisterNewCustomerCommandValidator, RegisterCustomerCommand>(command, cancellationToken))
        {
            return CommandResult.Failure<CustomerModel>(null, notificationHandler.GetNotifications());
        }

        Customer? customer = await uow.Customers.GetByEmail(command.Email!);

        if (customer is not null)
        {
            await HandleDomainViolationAsync(
                nameof(ApplicationConstants.Messages.CUSTOMER_EMAIL_DUPLICATED),
                ApplicationConstants.Messages.CUSTOMER_EMAIL_DUPLICATED,
                cancellationToken);

            return CommandResult.Failure<CustomerModel>(default, notificationHandler.GetNotifications());
        }

        customer = new Customer(command.Name!, command.Email!, command.Birthdate!.Value);

        await uow.Customers.AddAsync(customer, cancellationToken);

        if (await SaveChangesAsync(cancellationToken))
        {
            await publishEndpoint.Publish(
                new CustomerRegisteredEvent(
                    customer.Id,
                    customer.Name,
                    customer.Email,
                    customer.Birthdate),
                cancellationToken);

            return CommandResult.Success(
                typeAdapter.Adapt<CustomerModel>(customer));
        }

        return CommandResult.Failure<CustomerModel>(default, notificationHandler.GetNotifications());
    }

    public async Task<ICommandResult> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        if (!await ValidateCommandAsync<UpdateCustomerCommandValidator, UpdateCustomerCommand>(command, cancellationToken))
        {
            return CommandResult.Failure<CustomerModel>(default, notificationHandler.GetNotifications());
        }

        Customer? customer = await uow.Customers.LoadAsync(command.CustomerId!, cancellationToken);

        if (customer is null)
        {
            await HandleResourceNotFoundAsync(
                nameof(ApplicationConstants.Messages.CUSTOMER_NOT_FOUND),
                ApplicationConstants.Messages.CUSTOMER_NOT_FOUND,
                cancellationToken);

            return CommandResult.Failure<CustomerModel>(default, notificationHandler.GetNotifications());
        }

        Customer? existingCustomer = await uow.Customers.GetByEmail(customer.Email);

        if (existingCustomer is not null && existingCustomer != customer)
        {
            await HandleDomainViolationAsync(
                nameof(ApplicationConstants.Messages.CUSTOMER_EMAIL_DUPLICATED),
                ApplicationConstants.Messages.CUSTOMER_EMAIL_DUPLICATED,
                cancellationToken);

            return CommandResult.Failure<CustomerModel>(default, notificationHandler.GetNotifications());
        }

        customer.UpdateName(command.Name!);
        customer.UpdateBirthdate(command.Birthdate!.Value);

        uow.Customers.Update(customer);

        if (await SaveChangesAsync(cancellationToken))
        {
            await publishEndpoint.Publish(
                new CustomerUpdatedEvent(
                    customer.Id,
                    customer.Name,
                    customer.Email,
                    customer.Birthdate),
                cancellationToken);

            return CommandResult.Success();
        }

        return CommandResult.Failure<CustomerModel>(default, notificationHandler.GetNotifications());
    }

    public async Task<ICommandResult> Handle(RemoveCustomerCommand command, CancellationToken cancellationToken)
    {
        if (!await ValidateCommandAsync<RemoveCustomerCommandValidator, RemoveCustomerCommand>(command, cancellationToken))
        {
            return CommandResult.Failure<CustomerModel>(null, notificationHandler.GetNotifications());
        }

        Customer? customer = await uow.Customers.LoadAsync(command.CustomerId!, cancellationToken);

        if (customer is null)
        {
            await HandleResourceNotFoundAsync(
                nameof(ApplicationConstants.Messages.CUSTOMER_NOT_FOUND),
                ApplicationConstants.Messages.CUSTOMER_NOT_FOUND,
                cancellationToken);

            return CommandResult.Failure<CustomerModel>(null, notificationHandler.GetNotifications());
        }

        uow.Customers.Remove(customer);

        if (await SaveChangesAsync(cancellationToken))
        {
            await publishEndpoint.Publish(
                new CustomerRemovedEvent(command.CustomerId!),
                cancellationToken);

            return CommandResult.Success();
        }

        return CommandResult.Failure<CustomerModel>(default, notificationHandler.GetNotifications());
    }
}
