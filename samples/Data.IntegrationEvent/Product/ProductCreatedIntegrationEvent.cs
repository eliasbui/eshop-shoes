using Eshop.Core.Domain.Bases;

namespace Data.IntegrationEvent.Product;

public class ProductCreatedIntegrationEvent : EventBase
{
    public Guid Id { get; set; }
    private string Name { get; set; } = default!;
    public int Quantity { get; set; }
    public Guid ProductCodeId { get; set; }
    public decimal ProductCost { get; set; }

    public override void Flatten()
    {
        MetaData.Add("ProductId", Id);
        MetaData.Add("ProductName", Name);
        MetaData.Add("ProductQuantity", Quantity);
        MetaData.Add("ProductCode", ProductCodeId);
        MetaData.Add("ProductCost", ProductCost);
    }
}