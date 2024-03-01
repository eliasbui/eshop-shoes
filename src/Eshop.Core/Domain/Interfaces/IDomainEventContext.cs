using Eshop.Core.Domain.Bases;

namespace Eshop.Core.Domain.Interfaces;

public interface IDomainEventContext
{
    IEnumerable<EventBase> GetDomainEvents();
}