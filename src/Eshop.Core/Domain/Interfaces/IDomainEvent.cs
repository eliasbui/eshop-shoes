using MediatR;

namespace Eshop.Core.Domain.Interfaces;

public interface IDomainEvent : INotification
{
    DateTime CreatedAt { get; }
    IDictionary<string, object> MetaData { get; }
}