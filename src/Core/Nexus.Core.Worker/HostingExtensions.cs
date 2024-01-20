using System.Globalization;
using System.Text.Json.Serialization;
using Nexus.Core.Infra.IoC.Extensions;
using Nexus.Core.Worker.Consumers.Customers;
using Nexus.Core.Worker.Infra.Swagger;
using Nexus.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Localization;
using MassTransit;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using Nexus.Core.Worker.Consumers.Users;

namespace Nexus.Core.Worker;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc.ConfigureLogging(builder.Configuration, builder.Environment));

        builder.Services.ConfigureWorkerServices(builder.Configuration, builder.Environment);

        builder.Services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();
            x.AddConsumer<CustomerRegisteredEventConsumer, CustomerRegisteredEventConsumer.ConsumerDefinition>();
            x.AddConsumer<CustomerRemovedEventConsumer, CustomerRemovedEventConsumer.ConsumerDefinition>();
            x.AddConsumer<CustomerUpdatedEventConsumer, CustomerUpdatedEventConsumer.ConsumerDefinition>();

            x.AddConsumer<UserAccountCreatedEventConsumer, UserAccountCreatedEventConsumer.ConsumerDefinition>();
            x.AddConsumer<UserProfileUpdatedEventConsumer, UserProfileUpdatedEventConsumer.ConsumerDefinition>();

            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "dev", includeNamespace: false));

            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                configurator.UseDelayedMessageScheduler();
                configurator.ServiceInstance(instance =>
                {
                    instance.ConfigureJobServiceEndpoints();
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        builder.Services
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
            })
            .AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.MigrateWorkerDbContext();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nexus Core Worker v1");
                c.DisplayRequestDuration();
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(ApplicationCultures.Portugues, ApplicationCultures.Portugues),
            SupportedCultures = new List<CultureInfo>
            {
                ApplicationCultures.Portugues,
            },
            SupportedUICultures = new List<CultureInfo>
            {
                ApplicationCultures.Portugues,
            }
        });

        app.MapControllers();

        return app;
    }
}