using Eshop.Infrastructure.Bus.Darp;
using Eshop.Infrastructure.Bus.Darp.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Infrastructure.Bus;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(this IMvcBuilder mvcBuilder,
        IConfiguration config,
        string messageBrokerType = "dapr")
    {
        switch (messageBrokerType)
        {
            case "dapr":
                mvcBuilder.Services.Configure<DaprEventBusOptions>(config.GetSection(DaprEventBusOptions.Name));
                mvcBuilder.AddDapr();
                mvcBuilder.Services.AddScoped<IEventBus, DaprEventBus>();
                break;
        }

        return mvcBuilder.Services;
    }
}