using Nexus.Core.Domain.Companies.Aggregates;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Application.Companies.CreateCompany;

public static class CompanyFactory
{
    public static Company CreateNewCompany(CreateCompanyCommand command)
    {
        var company = new Company(command.CompanyName, command.BrandingName, command.Cnpj);

        PersonAddress address = company.AddAddress(
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
            // company.AddDocument(DocumentType.MunicipalRegistration, municipalRegistration);
        }

        if (!string.IsNullOrWhiteSpace(stateRegistration))
        {
            // company.AddDocument(DocumentType.StateRegistration, stateRegistration);
        }
    }
}

