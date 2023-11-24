using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nexus.Infra.Crosscutting.Constants;

namespace Nexus.Core.Application.Commands.Users.Validators
{
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                    .WithMessage(ApplicationConstants.Messages.USER_ID_REQUIRED)
                    .WithErrorCode(nameof(ApplicationConstants.Messages.USER_ID_REQUIRED));

            RuleFor(p => p.Biography)
                .MaximumLength(1024)
                    .WithMessage(ApplicationConstants.Messages.USER_BIOGRAPHY_MAXIMUM_LENGTH)
                    .WithErrorCode(nameof(ApplicationConstants.Messages.USER_BIOGRAPHY_MAXIMUM_LENGTH))
                .When(command => !string.IsNullOrWhiteSpace(command.Biography));

            RuleFor(p => p.Headline)
                .MaximumLength(128)
                    .WithMessage(ApplicationConstants.Messages.USER_HEADLINE_MAXIMUM_LENGTH)
                    .WithErrorCode(nameof(ApplicationConstants.Messages.USER_HEADLINE_MAXIMUM_LENGTH))
                .When(command => !string.IsNullOrWhiteSpace(command.Headline));
        }
    }
}