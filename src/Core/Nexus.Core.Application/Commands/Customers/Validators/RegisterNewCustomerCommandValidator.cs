using FluentValidation;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Commands.Customers.Validators;

public class RegisterNewCustomerCommandValidator :  AbstractValidator<RegisterNewCustomerCommand>
{
    public RegisterNewCustomerCommandValidator()
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
            .EmailAddress()
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_EMAIL_INVALID)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_EMAIL_INVALID));
    }

    private void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_NAME_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_NAME_REQUIRED))
            .Length(2, 150)
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_NAME_LENGTH_INVALID)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_NAME_LENGTH_INVALID));
    }

    private void ValidateBirthdate()
    {
        RuleFor(c => c.Birthdate)
            .NotEmpty()
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_REQUIRED)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_REQUIRED))
            .Must(HaveMinimumAge)
                .WithMessage(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_LENGTH_INVALID)
                .WithErrorCode(nameof(ApplicationConstants.Messages.CUSTOMER_BIRTHDATE_LENGTH_INVALID));
    }

    private static bool HaveMinimumAge(DateTime Birthdate)
        => Birthdate.Date <= DateTime.Today.AddYears(-18);
}
