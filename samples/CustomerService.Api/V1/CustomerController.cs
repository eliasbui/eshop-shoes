using CustomerService.Application.UseCases.Commands;
using Eshop.Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.V1
{
    [ApiVersion("1.0")]
    public class CustomerController(ISender? mediator) : BaseController(mediator)
    {
        [ApiVersion("1.0")]
        [HttpPost("/api/v{version:apiVersion}/customers")]
        public async Task<ActionResult> HandleAsync([FromBody] CreateCustomer.Command request,
            CancellationToken cancellationToken = new())
        {
            if (Mediator != null) return Ok(await Mediator.Send(request, cancellationToken).ConfigureAwait(false));
            return BadRequest("Mediator is not available");
        }
    }
}