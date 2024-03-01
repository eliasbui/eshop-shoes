namespace Eshop.Infrastructure.TransactionalOutbox.Darp;

public interface ITransactionalOutboxProcessor
{
    Task HandleAsync(Type integrationAssemblyType, CancellationToken cancellationToken = new());
}