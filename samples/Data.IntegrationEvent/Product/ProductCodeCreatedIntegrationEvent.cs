using Eshop.Core.Domain.Bases;

namespace Data.IntegrationEvent.Product;

public class ProductCodeCreatedIntegrationEvent : EventBase
{
    public Guid ProductCodeId { get; set; }
    private string ProductCodeName { get; set; } = default!;

    public override void Flatten()
    {
        MetaData.Add("ProductCodeId", ProductCodeId);
        MetaData.Add("ProductCodeName", ProductCodeName);
    }
}