using System.Text.Json.Serialization;
using Asp.Versioning;
using Goal.Infra.Crosscutting.Localization;
using MassTransit;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Logging;
using Nexus.Finance.Api.Infra.OpenApi;
using Nexus.Finance.Infra.IoC.Extensions;
using Nexus.Infra.Crosscutting.Extensions;
using Nexus.Infra.Http.JsonNamePolicies;
using Nexus.Infra.Http.ValueProviders;
using Scalar.AspNetCore;
using Serilog;

namespace Nexus.Finance.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((_, lc) => lc.ConfigureLogging(builder.Configuration, builder.Environment));

        builder.Services.ConfigureApiServices(builder.Configuration, builder.Environment);

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

        builder.Services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddDocumentTransformer<ServerHostTransformer>();
            options.AddOperationTransformer<SnakeCaseQueryOperationTransformer>();
            options.AddSchemaTransformer<SnakeCaseSchemaTransformer>();
        });

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

        if (app.Environment.IsDevelopment())
        {
            IdentityModelEventSource.ShowPII = true;

            app.MapOpenApi();
            app.MapScalarApiReference("api-docs", options =>
            {
                // Fluent API
                options
                    .WithTitle("Finance API")
                    .WithDefaultHttpClient(ScalarTarget.Http, ScalarClient.Http11)
                    .WithOAuth2Authentication(oauth =>
                    {
                        oauth.ClientId = app.Configuration["Keycloak:Resource"];
                        oauth.Scopes = app.Configuration["Keycloak:Scopes"]?.Split(' ');
                    });
            });
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.MapStaticAssets();
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