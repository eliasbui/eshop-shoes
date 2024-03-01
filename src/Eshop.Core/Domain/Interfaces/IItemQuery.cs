using System.Collections.Generic;

namespace Eshop.Core.Domain.Interfaces;

public interface IItemQuery<TId, TResponse> : IQuery<TResponse>
    where TId : struct
    where TResponse : notnull
{
    public List<string> Includes { get; init; }
    public TId Id { get; init; }
}