using Microsoft.OpenApi.Models;
using Nexus.Infra.Crosscutting.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nexus.Infra.Http.Swagger;

public class SnakeCaseQueryOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (OpenApiParameter item in operation.Parameters ?? [])
        {
            item.Name = item.Name.ToSnakeCase();
        }
    }
}
