using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Finance.Api.Infra.OpenApi;

internal class SnakeCaseQueryOperationTransformer : IOpenApiOperationTransformer
{
    public async Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        foreach (OpenApiParameter item in operation.Parameters ?? [])
        {
            item.Name = item.Name.ToSnakeCase();
        }

        await Task.CompletedTask;
    }
}