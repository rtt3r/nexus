using System.Reflection;
using Goal.Infra.Http.Swagger;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nexus.Core.Worker.Infra.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Nexus Core Worker", Version = "v1" });
        options.IncludeXmlComments(GetXmlCommentsFile());
        options.DocumentFilter<LowerCaseDocumentFilter>();
        options.DescribeAllParametersInCamelCase();
    }

    private static string GetXmlCommentsFile()
    {
        string xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
        return Path.Combine(AppContext.BaseDirectory, xmlFile);
    }
}
