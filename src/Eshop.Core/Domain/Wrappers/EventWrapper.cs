using Eshop.Core.Domain.Interfaces;
using MediatR;

namespace Eshop.Core.Domain.Wrappers;

public class EventWrapper(IDomainEvent @event) : INotification
{
    public IDomainEvent Event { get; } = @event;
}