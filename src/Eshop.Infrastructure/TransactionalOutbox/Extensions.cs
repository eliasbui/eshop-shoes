using Eshop.Core.Domain.Wrappers;
using Eshop.Infrastructure.TransactionalOutbox.Darp;
using Eshop.Infrastructure.TransactionalOutbox.Darp.Internal;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Infrastructure.TransactionalOutbox;

public static class Extensions
{
    public static IServiceCollection AddTransactionalOutbox(this IServiceCollection services, IConfiguration config,
        string provider = "dapr")
    {
        switch (provider)
        {
            case "dapr":
            {
                services.Configure<DaprTransactionalOutboxOptions>(
                    config.GetSection(DaprTransactionalOutboxOptions.Name));
                services.AddScoped<INotificationHandler<EventWrapper>, LocalDispatchedHandler>();
                services.AddScoped<ITransactionalOutboxProcessor, TransactionalOutboxProcessor>();
                break;
            }
        }

        return services;
    }
}