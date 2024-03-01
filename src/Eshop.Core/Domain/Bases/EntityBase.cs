using System;

namespace Eshop.Core.Domain.Bases;

public class EntityBase
{
    public Guid Id { get; protected init; } = Guid.NewGuid();
    public DateTime Created { get; protected init; } = DateTime.UtcNow;
    public DateTime? Updated { get; protected set; }
}