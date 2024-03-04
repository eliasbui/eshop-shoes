using System.Linq.Expressions;
using CustomerService.Core.Entities;
using Eshop.Core.Domain.Specification;

namespace CustomerService.Core.Specs
{
    public class CreditCardAvailableSpec(DateTime dateTime) : SpecificationBase<CreditCard>
    {
        public override Expression<Func<CreditCard, bool>?> Criteria =>
            creditCard => creditCard.Active && creditCard.Expiry >= dateTime;
    }
}