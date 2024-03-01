using System.Data;
using System.Text.Json;
using Eshop.Core.Domain.Interfaces;
using Eshop.Core.Domain.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eshop.Infrastructure.EfCore;

public class TxBehavior<TRequest, TResponse>(
    IDbFacadeResolver dbFacadeResolver,
    IDomainEventContext domainEventContext,
    IMediator mediator,
    ILogger<TxBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IDomainEventContext _domainEventContext = domainEventContext ?? throw new ArgumentNullException(nameof(domainEventContext));
    private readonly IDbFacadeResolver _dbFacadeResolver = dbFacadeResolver ?? throw new ArgumentNullException(nameof(dbFacadeResolver));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ILogger<TxBehavior<TRequest, TResponse>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (request is not ITxRequest)
        {
            return await next();
        }

        _logger.LogInformation("{Prefix} Handled command {MediatRRequest}", nameof(TxBehavior<TRequest, TResponse>),
            typeof(TRequest).FullName);
        _logger.LogDebug("{Prefix} Handled command {MediatRRequest} with content {RequestContent}",
            nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName, JsonSerializer.Serialize(request));
        _logger.LogInformation("{Prefix} Open the transaction for {MediatRRequest}",
            nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName);
        var strategy = _dbFacadeResolver.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            // Achieving atomicity
            await using var transaction =
                await _dbFacadeResolver.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            var response = await next();
            _logger.LogInformation("{Prefix} Executed the {MediatRRequest} request",
                nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName);

            await transaction.CommitAsync(cancellationToken);

            var domainEvents = _domainEventContext.GetDomainEvents().ToList();
            _logger.LogInformation("{Prefix} Published domain events for {MediatRRequest}",
                nameof(TxBehavior<TRequest, TResponse>), typeof(TRequest).FullName);

            var tasks = domainEvents
                .Select(async @event =>
                {
                    await _mediator.Publish(new EventWrapper(@event), cancellationToken);
                    _logger.LogDebug(
                        "{Prefix} Published domain event {DomainEventName} with payload {DomainEventContent}",
                        nameof(TxBehavior<TRequest, TResponse>), @event.GetType().FullName,
                        JsonSerializer.Serialize(@event));
                });

            await Task.WhenAll(tasks);

            return response;
        });
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        return await Handle(request, cancellationToken, next);
    }
}