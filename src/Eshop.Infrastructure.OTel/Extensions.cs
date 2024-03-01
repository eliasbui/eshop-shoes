using Eshop.Infrastructure.OTel.MediatR;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;

namespace Eshop.Infrastructure.OTel
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomOtelWithZipkin(this IServiceCollection services,
            IConfiguration config, Action<ZipkinExporterOptions> configureZipkin = null)
        {
            services.AddOpenTelemetry().WithTracing(b => b
                .SetSampler(new AlwaysOnSampler())
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddGrpcClientInstrumentation()
                .AddSqlClientInstrumentation(o => o.SetDbStatementForText = true)
                .AddSource(OTelMediatROptions.OTelMediatRName)
                .AddZipkinExporter(o =>
                {
                    config.Bind("OtelZipkin", o);
                    configureZipkin?.Invoke(o);
                })
                .Build());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(OTelMediatRTracingBehavior<,>));

            return services;
        }
    }
}