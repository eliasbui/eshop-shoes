using System.Collections.Generic;
using Eshop.Core.Domain.Interfaces;

namespace Eshop.Core.Domain.Bases;

public class EntityRootBase : EntityBase, IAggregateRoot
{
    public HashSet<EventBase> DomainEvents { get; private set; }

    public void AddDomainEvent(EventBase eventItem)
    {
        DomainEvents ??= new HashSet<EventBase>();
        DomainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(EventBase eventItem)
    {
        DomainEvents?.Remove(eventItem);
    }
}