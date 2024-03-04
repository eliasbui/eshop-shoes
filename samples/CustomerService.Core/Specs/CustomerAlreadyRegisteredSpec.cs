using System.Linq.Expressions;
using CustomerService.Core.Entities;
using Eshop.Core.Domain.Specification;

namespace CustomerService.Core.Specs
{
    public class CustomerAlreadyRegisteredSpec(string email) : SpecificationBase<Customer>
    {
        public override Expression<Func<Customer, bool>?> Criteria => customer => customer.Email == email;
    }
}