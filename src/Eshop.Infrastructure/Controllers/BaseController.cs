using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Infrastructure.Controllers;

public class BaseController(ISender mediator) : Controller
{
    protected ISender? Mediator => mediator ??= HttpContext.RequestServices.GetService<ISender>()!;
}