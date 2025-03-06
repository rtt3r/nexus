using FluentValidation;
using Nexus.Infra.Crosscutting.Validations.Fluent;
using static Nexus.Infra.Crosscutting.Constants.Notifications.BusinessGroup;

namespace Nexus.Core.Application.BusinessGroups.UpdateBusinessGroup;

internal sealed class UpdateBusinessGroupValidator : AbstractValidator<UpdateBusinessGroupCommand>
{
    public UpdateBusinessGroupValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithNotification(BUSINESS_GROUP_NAME_REQUIRED)
            .DependentRules(() =>
            {
                RuleFor(x => x.Name)
                    .Length(1, 128).WithNotification(BUSINESS_GROUP_NAME_LENGTH);
            });

        RuleFor(c => c.Description)
            .NotEmpty().WithNotification(BUSINESS_GROUP_DESCRIPTION_LENGTH)
            .When(p => !string.IsNullOrWhiteSpace(p.Description));

        RuleFor(c => c.TaxId)
            .NotEmpty().WithNotification(BUSINESS_GROUP_TAX_ID_LENGTH)
            .When(p => !string.IsNullOrWhiteSpace(p.TaxId));
    }
}
