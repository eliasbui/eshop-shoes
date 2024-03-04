using Eshop.Core.Domain.Attributes;
using Eshop.Core.Domain.Bases;

namespace Data.IntegrationEvent.Customer;

[DaprPubSubName(PubSubName = "pubsub")]
public class CustomerCreatedIntegrationEvent : EventBase
{
    public override void Flatten()
    {
    }
}