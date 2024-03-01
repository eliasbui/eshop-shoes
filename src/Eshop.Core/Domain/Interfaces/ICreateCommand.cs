namespace Eshop.Core.Domain.Interfaces;

public interface ICreateCommand<TRequest, TResponse> : ICommand<TResponse>, ITxRequest
    where TRequest : notnull
    where TResponse : notnull
{
    public TRequest Model { get; init; }
}