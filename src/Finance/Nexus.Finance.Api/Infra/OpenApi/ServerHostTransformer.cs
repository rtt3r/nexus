using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Nexus.Finance.Api.Infra.OpenApi;

internal sealed class ServerHostTransformer : IOpenApiDocumentTransformer
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public ServerHostTransformer(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Servers = [
            new OpenApiServer
            {
                Url = $"{httpContextAccessor.HttpContext?.Request.Scheme}://{httpContextAccessor.HttpContext?.Request.Host.Value}"
            }
        ];

        await Task.CompletedTask;
    }
}