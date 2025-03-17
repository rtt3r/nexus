using FluentValidation;
using Nexus.Infra.Crosscutting.Validations.Fluent;
using static Nexus.Infra.Crosscutting.Constants.Notifications.Addresses;
using static Nexus.Infra.Crosscutting.Constants.Notifications.Companies;
using static Nexus.Infra.Crosscutting.Constants.Notifications.Contacts;

namespace Nexus.Core.Application.Companies.CreateCompany;

internal sealed class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithNotification(COMPANY_NAME_REQUIRED)
            .MaximumLength(100).WithNotification(COMPANY_NAME_MAX_LENGTH);

        RuleFor(x => x.BrandingName)
            .NotEmpty().WithNotification(BRANDING_NAME_REQUIRED)
            .MaximumLength(100).WithNotification(BRANDING_NAME_MAX_LENGTH);

        RuleFor(x => x.Cnpj)
            .NotEmpty().WithNotification(CNPJ_REQUIRED)
            .Cnpj().WithNotification(CNPJ_INVALID_FORMAT);

        RuleFor(x => x.Address)
            .NotNull().WithNotification(ADDRESS_REQUIRED)
            .DependentRules(() =>
            {
                RuleFor(x => x.Address.ZipCode)
                    .NotEmpty().WithNotification(ZIP_CODE_REQUIRED)
                    .Matches("^[0-9]{8}$").WithNotification(ZIP_CODE_INVALID_FORMAT);

                RuleFor(x => x.Address.Street)
                    .NotEmpty().WithNotification(STREET_REQUIRED)
                    .MaximumLength(150).WithNotification(STREET_MAX_LENGTH);

                RuleFor(x => x.Address.Number)
                    .NotEmpty().WithNotification(NUMBER_REQUIRED)
                    .MaximumLength(10).WithNotification(NUMBER_MAX_LENGTH);

                RuleFor(x => x.Address.Neighborhood)
                    .NotEmpty().WithNotification(NEIGHBORHOOD_REQUIRED)
                    .MaximumLength(100).WithNotification(NEIGHBORHOOD_MAX_LENGTH);

                RuleFor(x => x.Address.City)
                    .NotEmpty().WithNotification(CITY_REQUIRED)
                    .MaximumLength(100).WithNotification(CITY_MAX_LENGTH);

                RuleFor(x => x.Address.State)
                    .NotEmpty().WithNotification(STATE_REQUIRED)
                    .MaximumLength(2).WithNotification(STATE_MAX_LENGTH);

                RuleFor(x => x.Address.Country)
                    .NotEmpty().WithNotification(COUNTRY_REQUIRED)
                    .MaximumLength(100).WithNotification(COUNTRY_MAX_LENGTH);

                RuleFor(x => x.Address.Complement)
                    .MaximumLength(100).WithNotification(COMPLEMENT_MAX_LENGTH)
                    .When(x => !string.IsNullOrEmpty(x.Address.Complement));
            });

        RuleFor(x => x.Contacts)
            .NotNull().WithNotification(CONTACTS_REQUIRED)
            .NotEmpty().WithNotification(CONTACTS_REQUIRED)
            .DependentRules(() =>
            {
                RuleForEach(x => x.Contacts).SetValidator(new CreateCompanyContactCommandValidator());
            });
    }
}

internal sealed class CreateCompanyContactCommandValidator : AbstractValidator<CreateCompanyCommand.CreateCompanyContactCommand>
{
    public CreateCompanyContactCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithNotification(CONTACT_NAME_REQUIRED)
            .MaximumLength(100).WithNotification(CONTACT_NAME_MAX_LENGTH);

        RuleFor(x => x.Email)
            .EmailAddress().WithNotification(EMAIL_INVALID_FORMAT)
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.LandlinePhone)
            .Matches(@"^\d{10,11}$").WithNotification(LANDLINE_PHONE_INVALID_FORMAT)
            .When(x => !string.IsNullOrEmpty(x.LandlinePhone));

        RuleFor(x => x.MobilePhone)
            .Matches(@"^\d{10,11}$").WithNotification(MOBILE_PHONE_INVALID_FORMAT)
            .When(x => !string.IsNullOrEmpty(x.MobilePhone));

        RuleFor(x => x.Whatsapp)
            .Matches(@"^\d{10,11}$").WithNotification(WHATSAPP_INVALID_FORMAT)
            .When(x => !string.IsNullOrEmpty(x.Whatsapp));

        RuleFor(x => x)
            .Must(contact =>
                !string.IsNullOrEmpty(contact.LandlinePhone) ||
                !string.IsNullOrEmpty(contact.MobilePhone) ||
                !string.IsNullOrEmpty(contact.Whatsapp) ||
                !string.IsNullOrEmpty(contact.Email))
            .WithNotification(CONTACT_AT_LEAST_ONE_REQUIRED);
    }
}
