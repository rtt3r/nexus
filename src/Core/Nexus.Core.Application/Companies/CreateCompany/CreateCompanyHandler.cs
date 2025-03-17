using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Adapters;
using MassTransit;
using Nexus.Core.Domain.Companies.Aggregates;
using Nexus.Core.Domain.Companies.Events;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Core.Infra.Data;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Crosscutting.Extensions;
using OneOf;
using OneOf.Types;
using static Nexus.Infra.Crosscutting.Constants.Notifications.Companies;
using CompanyDto = Nexus.Core.Model.Companies.Company;

namespace Nexus.Core.Application.Companies.CreateCompany;

internal class CreateCompanyCommandHandler(
    ICoreUnitOfWork uow,
    ITypeAdapter typeAdapter,
    IPublishEndpoint publishEndpoint,
    AppState appState)
    : CommandHandler(uow, publishEndpoint, typeAdapter),
    ICommandHandler<CreateCompanyCommand, OneOf<CompanyDto, AppError>>
{
    private readonly AppState appState = appState;

    public async Task<OneOf<CompanyDto, AppError>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        OneOf<None, InputValidationError> validation = await ValidateCommandAsync<CreateCompanyCommandValidator, CreateCompanyCommand>(command, cancellationToken);

        if (validation.IsError())
        {
            return validation.GetError();
        }

        Company? existingCompany = await uow.Companies.GetByCnpjAsync(command.Cnpj, cancellationToken);

        if (existingCompany is { Active: true })
        {
            return new BusinessRuleError(COMPANY_NAME_DUPLICATED);
        }

        Company company = CreateNewCompany(command);

        await uow.Companies.AddAsync(company, cancellationToken);
        await CommitAsync(cancellationToken);

        await RaiseEvent(new CompanyCreatedEvent(company.Id, appState.User!.UserId), cancellationToken);

        return ProjectAs<CompanyDto>(company);
    }

    private static Company CreateNewCompany(CreateCompanyCommand command)
    {
        var company = new Company(command.CompanyName, command.BrandingName, command.Cnpj);

        Address address = company.AddAddress(
            AddressType.Principal,
            command.Address.ZipCode,
            command.Address.Street,
            command.Address.Number,
            command.Address.Neighborhood,
            command.Address.City,
            command.Address.State,
            command.Address.Country);

        if (!string.IsNullOrWhiteSpace(command.Address.Complement))
        {
            address.SetComplement(command.Address.Complement);
        }

        AddContacts(company, command.Contacts);
        AddDocuments(company, command.MunicipalRegistration, command.StateRegistration);

        if (!string.IsNullOrWhiteSpace(command.Logo))
        {
            company.SetLogo(command.Logo);
        }

        return company;
    }

    private static void AddContacts(Company company, IEnumerable<CreateCompanyCommand.CreateCompanyContactCommand> contacts)
    {
        foreach (CreateCompanyCommand.CreateCompanyContactCommand contact in contacts)
        {
            company.AddContact(
                ContactType.Primary,
                contact.Name,
                contact.Email,
                contact.LandlinePhone,
                contact.MobilePhone,
                contact.Whatsapp);
        }
    }

    private static void AddDocuments(Company company, string? municipalRegistration, string? stateRegistration)
    {
        if (!string.IsNullOrWhiteSpace(municipalRegistration))
        {
            company.AddDocument(DocumentType.MunicipalRegistration, municipalRegistration);
        }

        if (!string.IsNullOrWhiteSpace(stateRegistration))
        {
            company.AddDocument(DocumentType.StateRegistration, stateRegistration);
        }
    }

}
