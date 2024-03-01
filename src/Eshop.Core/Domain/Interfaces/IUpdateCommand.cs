namespace Eshop.Core.Domain.Interfaces;

public interface IUpdateCommand<TRequest, TResponse> : ICommand<TResponse>, ITxRequest
    where TRequest : notnull
    where TResponse : notnull
{
    public TRequest Model { get; init; }
}