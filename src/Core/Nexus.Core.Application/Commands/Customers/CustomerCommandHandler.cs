using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Application.Commands.Customers.Validators;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.Customers.Events;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Exceptions;
using static Nexus.Infra.Crosscutting.Constants.ApplicationConstants;
using CustomerModel = Nexus.Core.Model.Customers.Customer;

namespace Nexus.Core.Application.Commands.Customers;

public class CustomerCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, typeAdapter),
    ICommandHandler<RegisterCustomerCommand, CustomerModel>,
    ICommandHandler<UpdateCustomerCommand>,
    ICommandHandler<RemoveCustomerCommand>
{
    private readonly IPublishEndpoint publishEndpoint = publishEndpoint;
    private readonly AppState appState = appState;

    public async Task<CustomerModel> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        await ValidateCommandAsync<RegisterCustomerCommandValidator, RegisterCustomerCommand>(command, cancellationToken);

        Customer? customer = await uow.Customers.GetByEmail(command.Email!);

        if (customer is not null)
        {
            throw new DomainViolationException(nameof(Messages.CUSTOMER_EMAIL_DUPLICATED), Messages.CUSTOMER_EMAIL_DUPLICATED);
        }

        customer = new Customer(command.Name!, command.Email!, command.Birthdate!.Value);

        await uow.Customers.AddAsync(customer, cancellationToken);

        await SaveChangesAsync(cancellationToken);

        await publishEndpoint.Publish(
            new CustomerCreatedEvent(customer.Id, appState.User!.UserId),
            cancellationToken);

        return ProjectAs<CustomerModel>(customer);
    }

    public async Task Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        await ValidateCommandAsync<UpdateCustomerCommandValidator, UpdateCustomerCommand>(command, cancellationToken);

        Customer? customer = await uow.Customers.LoadAsync(command.CustomerId!, cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(Messages.CUSTOMER_NOT_FOUND), Messages.CUSTOMER_NOT_FOUND);

        if (await uow.Customers.HasAnotherWithEmailAsync(command.CustomerId!, command.Email!))
        {
            throw new ResourceNotFoundException(nameof(Messages.CUSTOMER_EMAIL_DUPLICATED), Messages.CUSTOMER_EMAIL_DUPLICATED);
        }

        customer.UpdateName(command.Name!);
        customer.UpdateBirthdate(command.Birthdate!.Value);
        customer.UpdateEmail(command.Email!);

        uow.Customers.Update(customer);

        await SaveChangesAsync(cancellationToken);

        await publishEndpoint.Publish(
            new CustomerUpdatedEvent(customer.Id, appState.User!.UserId),
            cancellationToken);
    }

    public async Task Handle(RemoveCustomerCommand command, CancellationToken cancellationToken)
    {
        await ValidateCommandAsync<RemoveCustomerCommandValidator, RemoveCustomerCommand>(command, cancellationToken);

        Customer? customer = await uow.Customers.LoadAsync(command.CustomerId!, cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(Messages.CUSTOMER_NOT_FOUND), Messages.CUSTOMER_NOT_FOUND);

        uow.Customers.Remove(customer);

        await SaveChangesAsync(cancellationToken);

        await publishEndpoint.Publish(
            new CustomerRemovedEvent(command.CustomerId!, appState.User!.UserId),
            cancellationToken);
    }
}
