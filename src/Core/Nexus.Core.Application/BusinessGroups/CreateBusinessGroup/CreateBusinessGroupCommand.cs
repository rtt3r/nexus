using Goal.Application.Commands;
using Nexus.Core.Model.BusinessGroups;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;

namespace Nexus.Core.Application.BusinessGroups.CreateBusinessGroup;

public class CreateBusinessGroupCommand : ICommand<OneOf<BusinessGroup, AppError>>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? TaxId { get; set; }
}
