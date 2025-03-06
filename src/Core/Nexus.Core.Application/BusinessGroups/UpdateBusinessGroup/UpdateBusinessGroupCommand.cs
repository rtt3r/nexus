using Goal.Application.Commands;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Application.BusinessGroups.UpdateBusinessGroup;

public class UpdateBusinessGroupCommand : ICommand<OneOf<None, AppError>>
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? TaxId { get; set; }
}
