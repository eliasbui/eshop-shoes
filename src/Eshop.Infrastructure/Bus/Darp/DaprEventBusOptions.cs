namespace Eshop.Infrastructure.Bus.Darp;

public class DaprEventBusOptions
{
    public static string Name = "DaprEventBus";
    public string PubSubName { get; set; } = "pubsub";
}