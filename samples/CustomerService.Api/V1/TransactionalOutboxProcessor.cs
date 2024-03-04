using System.Runtime.Serialization.DataContracts;
using Eshop.Infrastructure.Controllers;
using Eshop.Infrastructure.TransactionalOutbox.Darp;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.V1
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TransactionalOutboxProcessor(ITransactionalOutboxProcessor outboxProcessor) : BaseController(null)
    {
        private readonly ITransactionalOutboxProcessor _outboxProcessor =
            outboxProcessor ?? throw new ArgumentNullException(nameof(outboxProcessor));

        [HttpPost("CustomerOutboxCron")]
        public async Task<ActionResult> HandleProductOutboxCronAsync(CancellationToken cancellationToken = new())
        {
            // await _outboxProcessor.HandleAsync(typeof(ImportAnchor), cancellationToken);

            return Ok();
        }
    }
}