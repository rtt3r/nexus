using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Finance.Api.Infra.OpenApi;

internal class SnakeCaseSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (schema.Properties?.Count > 0)
        {
            schema.Properties = TransformProperties(schema.Properties);
        }

        return Task.CompletedTask;
    }

    private static IDictionary<string, OpenApiSchema> TransformProperties(IDictionary<string, OpenApiSchema> properties)
    {
        return properties?.ToDictionary(
            item => item.Key.ToSnakeCase(),
            item =>
            {
                if (item.Value.Properties?.Count > 0)
                {
                    item.Value.Properties = TransformProperties(item.Value.Properties);
                }
                return item.Value;
            }
        ) ?? [];
    }
}
