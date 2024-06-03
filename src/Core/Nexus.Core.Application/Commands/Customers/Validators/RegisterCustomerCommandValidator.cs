using FluentValidation;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Commands.Customers.Validators;

public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
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
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_EMAIL_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_EMAIL_REQUIRED))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email)
                        .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                            .WithMessage(ApplicationConstants.Messages.CUSTOMER_EMAIL_INVALID)
                            .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_EMAIL_INVALID));
                });
    }

    private void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_NAME_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_NAME_REQUIRED))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                        .Length(2, 150)
                            .WithMessage(ApplicationConstants.Messages.CUSTOMER_NAME_LENGTH_INVALID)
                            .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_NAME_LENGTH_INVALID));
                });
    }

    private void ValidateBirthdate()
    {
        RuleFor(c => c.Birthdate)
            .NotEmpty()
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_REQUIRED))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Birthdate)
                        .Must(HaveMinimumAge)
                            .WithMessage(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_LENGTH_INVALID)
                            .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_LENGTH_INVALID));
                });
    }

    private static bool HaveMinimumAge(DateOnly? Birthdate)
        => Birthdate!.Value <= DateOnly.FromDateTime(DateTime.Today).AddYears(-18);
}
