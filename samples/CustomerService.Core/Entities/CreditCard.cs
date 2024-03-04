using Eshop.Core.Domain.Bases;

namespace CustomerService.Core.Entities;

public class CreditCard : EntityRootBase
{
    public string NameOnCard { get; protected set; } = default!;
    public string CardNumber { get; protected set; } = default!;
    public bool Active { get; protected set; }
    public DateTime Expiry { get; protected set; }
    public Customer Customer { get; protected set; }
}