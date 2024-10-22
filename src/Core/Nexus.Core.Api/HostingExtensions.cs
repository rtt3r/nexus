using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Goal.Infra.Crosscutting.Localization;
using MassTransit;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Nexus.Core.Api.Swagger;
using Nexus.Core.Infra.IoC.Extensions;
using Nexus.Infra.Crosscutting.Extensions;
using Nexus.Infra.Http.JsonNamePolicies;
using Nexus.Infra.Http.ValueProviders;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nexus.Core.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((_, lc) => lc.ConfigureLogging(builder.Configuration, builder.Environment));

        builder.Services.ConfigureApiServices(builder.Configuration, builder.Environment);

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        builder.Services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                cfg.UseDelayedMessageScheduler();
                cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
            });
        });

        builder.Services
            .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        builder.Services
            .AddRouting(options => options.LowercaseUrls = true)
            .AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
                options.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureApiSwaggerOptions>();
        builder.Services.AddSwaggerGen();

        builder.Services.AddKeycloak(builder.Configuration);

        builder.Services.AddCors();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(ApplicationCultures.English, ApplicationCultures.English),
            SupportedCultures =
            [
                ApplicationCultures.English
            ],
            SupportedUICultures =
            [
                ApplicationCultures.English
            ]
        });

        app.UseSerilogRequestLogging();
        app.UseExceptionHandler();
        app.MigrateApiDbContext();

        if (app.Environment.IsDevelopment())
        {
            IdentityModelEventSource.ShowPII = true;

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers =
                    [
                        new OpenApiServer
                        {
                            Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"
                        }
                    ];
                });
            });
            app.UseSwaggerUI(c =>
            {
                IApiVersionDescriptionProvider apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (string groupName in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(p => p.GroupName))
                {
                    c.SwaggerEndpoint(
                        $"/swagger/{groupName}/swagger.json",
                        groupName.ToUpperInvariant());
                }

                c.OAuthClientId(app.Configuration["Keycloak:Resource"]);
                c.OAuthClientSecret(app.Configuration["Keycloak:Credentials:Secret"]);
                c.OAuthAppName("Nexus Core Api");

                c.DisplayRequestDuration();
                c.RoutePrefix = string.Empty;
            });
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();

        app.UseCors(builder =>
        {
            string[] origins = (app.Configuration["Cors:Origins"] ?? string.Empty)
                .Split(';', StringSplitOptions.RemoveEmptyEntries);

            builder
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });

        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}