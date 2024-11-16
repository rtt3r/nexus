using FluentValidation;
using Nexus.Core.Application.Customers.Commands;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Customers.Validators;

internal class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
    {
        ValidateName();
        ValidateBirthdate();
        ValidateEmail();
    }

    private void ValidateEmail()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
                .WithMessage(Notifications.Customer.CUSTOMER_EMAIL_REQUIRED.Message)
                .WithErrorCode(Notifications.Customer.CUSTOMER_EMAIL_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email)
                        .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                            .WithMessage(Notifications.Customer.CUSTOMER_EMAIL_INVALID.Message)
                            .WithErrorCode(Notifications.Customer.CUSTOMER_EMAIL_INVALID.Code);
                });
    }

    private void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage(Notifications.Customer.CUSTOMER_NAME_REQUIRED.Message)
                .WithErrorCode(Notifications.Customer.CUSTOMER_NAME_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                        .Length(2, 150)
                            .WithMessage(Notifications.Customer.CUSTOMER_NAME_LENGTH_INVALID.Message)
                            .WithErrorCode(Notifications.Customer.CUSTOMER_NAME_LENGTH_INVALID.Code);
                });
    }

    private void ValidateBirthdate()
    {
        RuleFor(c => c.Birthdate)
            .NotEmpty()
                .WithMessage(Notifications.Customer.CUSTOMER_BIRTHDATE_REQUIRED.Message)
                .WithErrorCode(Notifications.Customer.CUSTOMER_BIRTHDATE_REQUIRED.Code)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Birthdate)
                        .Must(HaveMinimumAge)
                            .WithMessage(Notifications.Customer.CUSTOMER_BIRTHDATE_LENGTH_INVALID.Message)
                            .WithErrorCode(Notifications.Customer.CUSTOMER_BIRTHDATE_LENGTH_INVALID.Code);
                });
    }

    private static bool HaveMinimumAge(DateOnly? Birthdate)
        => Birthdate!.Value <= DateOnly.FromDateTime(DateTime.Today).AddYears(-18);
}
