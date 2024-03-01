using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Eshop.Infrastructure.Auth;

public class AuthBehavior<TRequest, TResponse>(
    IAuthorizationService authorizationService,
    IEnumerable<IAuthorizationRequirement> authorizationRequirements,
    IHttpContextAccessor httpContextAccessor,
    ILogger<AuthBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not IAuthRequest)
        {
            return await next();
        }

        logger.LogInformation("[{Prefix}] Starting AuthBehavior", nameof(AuthBehavior<TRequest, TResponse>));
        var currentUser = httpContextAccessor.HttpContext?.User;
        if (currentUser == null)
        {
            throw new Exception("You need to login.");
        }

        var result = await authorizationService.AuthorizeAsync(
            httpContextAccessor.HttpContext!.User,
            null,
            authorizationRequirements.Where(x => x is TRequest));

        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException(result.Failure?.FailedRequirements.First().ToString());
        }

        return await next();
    }
}