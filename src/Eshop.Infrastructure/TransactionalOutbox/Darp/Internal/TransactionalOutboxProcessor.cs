using Dapr.Client;
using Eshop.Infrastructure.Bus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Eshop.Infrastructure.TransactionalOutbox.Darp.Internal
{
    internal class TransactionalOutboxProcessor(
        DaprClient daprClient,
        IEventBus eventBus,
        IOptions<DaprTransactionalOutboxOptions> options,
        ILogger<TransactionalOutboxProcessor> logger)
        : ITransactionalOutboxProcessor
    {
        private readonly DaprClient _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
        private readonly IEventBus _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        private readonly IOptions<DaprTransactionalOutboxOptions> _options = options ?? throw new ArgumentNullException(nameof(options));
        private readonly ILogger<TransactionalOutboxProcessor> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task HandleAsync(Type integrationAssemblyType, CancellationToken cancellationToken = new ())
        {
            _logger.LogTrace("{TransactionalOutboxProcessor}: Cron @{DateTime}", nameof(TransactionalOutboxProcessor), DateTime.UtcNow);

            var events = await _daprClient.GetStateEntryAsync<List<OutboxEntity>>(_options.Value.StateStoreName, _options.Value.OutboxName, cancellationToken: cancellationToken);

            if (events?.Value is not {Count: > 0}) return;

            var deletedEventIds = new List<Guid>();

            foreach (var domainEvent in events.Value)
            {
                if (domainEvent.Id.Equals(Guid.Empty) || string.IsNullOrEmpty(domainEvent.Type) || string.IsNullOrEmpty(domainEvent.Data)) continue;

                var @event = domainEvent.RecreateMessage(integrationAssemblyType.Assembly);

                await _eventBus.PublishAsync(@event, token: cancellationToken);

                deletedEventIds.Add(domainEvent.Id);
            }

            if (deletedEventIds.Count <= 0) return;

            foreach (var deletedEventId in deletedEventIds)
            {
                events.Value.RemoveAll(e => e.Id == deletedEventId);
            }

            await events.SaveAsync(cancellationToken: cancellationToken);
        }
    }
}