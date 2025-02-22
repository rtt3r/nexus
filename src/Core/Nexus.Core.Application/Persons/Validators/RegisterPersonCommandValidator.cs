using FluentValidation;
using Nexus.Core.Application.Persons.Commands;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Persons.Validators;

internal class RegisterPersonCommandValidator : AbstractValidator<RegisterNaturalPersonCommand>
{
    public RegisterPersonCommandValidator()
    {
        RuleFor(c => c.FisrtName)
            .NotEmpty()
                .WithMessage(Notifications.Person.PERSON_FIRST_NAME_REQUIRED.Message)
                .WithErrorCode(Notifications.Person.PERSON_FIRST_NAME_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.FisrtName)
                        .Length(2, 50)
                            .WithMessage(Notifications.Person.PERSON_FIRST_NAME_LENGTH.Message)
                            .WithErrorCode(Notifications.Person.PERSON_FIRST_NAME_LENGTH.Code);
                });

        RuleFor(c => c.LastName)
            .NotEmpty()
                .WithMessage(Notifications.Person.PERSON_LAST_NAME_REQUIRED.Message)
                .WithErrorCode(Notifications.Person.PERSON_LAST_NAME_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.LastName)
                        .Length(2, 50)
                            .WithMessage(Notifications.Person.PERSON_LAST_NAME_LENGTH.Message)
                            .WithErrorCode(Notifications.Person.PERSON_LAST_NAME_LENGTH.Code);
                });

        RuleFor(x => x.Addresses)
            .NotNull()
                .WithMessage(Notifications.Person.PERSON_ADDRESS_LIST_REQUIRED.Message)
                .WithErrorCode(Notifications.Person.PERSON_ADDRESS_LIST_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Addresses).NotEmpty()
                        .WithMessage(Notifications.Person.PERSON_ADDRESS_LIST_REQUIRED.Message)
                        .WithErrorCode(Notifications.Person.PERSON_ADDRESS_LIST_REQUIRED.Code);
                });

        RuleForEach(x => x.Addresses)
            .SetValidator(new RegisterPersonAddressCommandValidator());

        RuleFor(x => x.Emails)
            .NotNull()
                .WithMessage(Notifications.Person.PERSON_EMAIL_LIST_REQUIRED.Message)
                .WithErrorCode(Notifications.Person.PERSON_EMAIL_LIST_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Emails).NotEmpty()
                        .WithMessage(Notifications.Person.PERSON_EMAIL_LIST_REQUIRED.Message)
                        .WithErrorCode(Notifications.Person.PERSON_EMAIL_LIST_REQUIRED.Code);
                });

        RuleForEach(x => x.Emails)
            .SetValidator(new RegisterPersonEmailCommandValidator());

        RuleFor(x => x.PhoneNumbers)
            .NotNull()
                .WithMessage(Notifications.Person.PERSON_PHONE_NUMBER_LIST_REQUIRED.Message)
                .WithErrorCode(Notifications.Person.PERSON_PHONE_NUMBER_LIST_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.PhoneNumbers).NotEmpty()
                        .WithMessage(Notifications.Person.PERSON_PHONE_NUMBER_LIST_REQUIRED.Message)
                        .WithErrorCode(Notifications.Person.PERSON_PHONE_NUMBER_LIST_REQUIRED.Code);
                });

        RuleForEach(x => x.PhoneNumbers)
            .SetValidator(new RegisterPersonPhoneNumberCommandValidator());

        When(person => !string.IsNullOrWhiteSpace(person.Gender), () =>
        {
            RuleFor(x => x.Gender).IsEnumName(typeof(Gender));
        });

        When(person => person.Birthdate is not null, () =>
        {
            RuleFor(x => x.Birthdate)
                .Must(x => x!.Value <= DateOnly.FromDateTime(DateTime.Today).AddYears(-18))
                    .WithMessage(Notifications.Person.PERSON_GENDER_INVALID.Message)
                    .WithErrorCode(Notifications.Person.PERSON_GENDER_INVALID.Code);
        });
    }

    private class RegisterPersonAddressCommandValidator : AbstractValidator<RegisterNaturalPersonCommand.Address>
    {
        public RegisterPersonAddressCommandValidator()
        {
        }
    }

    private class RegisterPersonEmailCommandValidator : AbstractValidator<RegisterNaturalPersonCommand.Email>
    {
        public RegisterPersonEmailCommandValidator()
        {
            RuleFor(address => address.Address)
                .NotEmpty()
                    .WithMessage(Notifications.Person.PERSON_EMAIL_REQUIRED.Message)
                    .WithErrorCode(Notifications.Person.PERSON_EMAIL_REQUIRED.Code)
                    .DependentRules(() =>
                    {
                        RuleFor(email => email.Address)
                            .EmailAddress()
                                .WithMessage(Notifications.Person.PERSON_EMAIL_INVALID.Message)
                                .WithErrorCode(Notifications.Person.PERSON_EMAIL_INVALID.Code);
                    });
        }
    }

    private class RegisterPersonPhoneNumberCommandValidator : AbstractValidator<RegisterNaturalPersonCommand.PhoneNumber>
    {
        public RegisterPersonPhoneNumberCommandValidator()
        {
            RuleFor(phoneNumber => phoneNumber.CountryCode)
                .NotEmpty()
                    .WithMessage(Notifications.Person.PERSON_PHONE_COUNTRY_CODE_REQUIRED.Message)
                    .WithErrorCode(Notifications.Person.PERSON_PHONE_COUNTRY_CODE_REQUIRED.Code)
                    .DependentRules(() =>
                    {
                        RuleFor(email => email.CountryCode)
                            .EmailAddress()
                                .WithMessage(Notifications.Person.PERSON_PHONE_COUNTRY_CODE_INVALID.Message)
                                .WithErrorCode(Notifications.Person.PERSON_PHONE_COUNTRY_CODE_INVALID.Code);
                    });

            RuleFor(phoneNumber => phoneNumber.Number)
                .NotEmpty()
                    .WithMessage(Notifications.Person.PERSON_PHONE_NUMBER_REQUIRED.Message)
                    .WithErrorCode(Notifications.Person.PERSON_PHONE_NUMBER_REQUIRED.Code)
                    .DependentRules(() =>
                    {
                        RuleFor(email => email.Number)
                            .EmailAddress()
                                .WithMessage(Notifications.Person.PERSON_PHONE_NUMBER_INVALID.Message)
                                .WithErrorCode(Notifications.Person.PERSON_PHONE_NUMBER_INVALID.Code);
                    });
        }
    }
}