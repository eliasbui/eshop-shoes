using Dapr.Client;
using Eshop.Core.Domain.Wrappers;
using MediatR;
using Microsoft.Extensions.Options;

namespace Eshop.Infrastructure.TransactionalOutbox.Darp.Internal;

public class LocalDispatchedHandler : INotificationHandler<EventWrapper>
{
    private readonly DaprClient _daprClient;
    private readonly IOptions<DaprTransactionalOutboxOptions> _options;

    public async Task Handle(EventWrapper notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}