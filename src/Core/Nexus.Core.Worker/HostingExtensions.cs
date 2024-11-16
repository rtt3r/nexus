using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Infra.IoC.Extensions;
using Nexus.Infra.Crosscutting.Extensions;
using Serilog;

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
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumersFromNamespaceContaining(typeof(HostingExtensions));
            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(prefix: "dev", includeNamespace: false));

            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                configurator.UseDelayedMessageScheduler();
                configurator.ServiceInstance(instance =>
                {
                    instance.ConfigureJobServiceEndpoints();
                });
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("dev", false));
                configurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(5)));
            });
        });

        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseRouting();

        return app;
    }
}