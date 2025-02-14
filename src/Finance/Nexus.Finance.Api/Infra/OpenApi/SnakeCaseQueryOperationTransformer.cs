using Goal.Infra.Crosscutting.Extensions;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Finance.Api.Infra.OpenApi;

internal class SnakeCaseQueryOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        if (operation.Parameters is null)
        {
            return Task.CompletedTask;
        }

        operation.Parameters.ForEach(p => p.Name = p.Name.ToSnakeCase());

        return Task.CompletedTask;
    }
}
