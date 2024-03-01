using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;

namespace Eshop.Infrastructure.OTel.MediatR;

public class OTelMediatRTracingBehavior<TRequest, TResponse>(
    IHttpContextAccessor httpContextAccessor,
    ILogger<OTelMediatRTracingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private static readonly ActivitySource ActivitySource = new(OTelMediatROptions.OTelMediatRName);

    private readonly IHttpContextAccessor _httpContextAccessor =
        httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    private readonly ILogger<OTelMediatRTracingBehavior<TRequest, TResponse>> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<TResponse> Handle(TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var traceId = Activity.Current?.TraceId.ToString() ?? _httpContextAccessor?.HttpContext?.TraceIdentifier;
        const string prefix = nameof(OTelMediatRTracingBehavior<TRequest, TResponse>);
        var handlerName = typeof(TRequest).Name.Replace("Query", "Handler"); // by convention

        _logger.LogInformation(
            "[{Prefix}:{HandlerName}] Handle {X-RequestData} request with TraceId={TraceId}",
            prefix, handlerName, typeof(TRequest).Name, traceId);

        using var activity = ActivitySource.StartActivity($"{OTelMediatROptions.OTelMediatRName}.{handlerName}",
            ActivityKind.Server);

        activity?.AddEvent(new ActivityEvent(handlerName))
            ?.AddTag("params.request.name", typeof(TRequest).Name)
            ?.AddTag("params.response.name", typeof(TResponse).Name);

        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            activity.SetStatus(OpenTelemetry.Trace.Status.Error.WithDescription(ex.Message));
            activity.RecordException(ex);

            _logger.LogError(ex, "[{Prefix}:{HandlerName}] {ErrorMessage}", prefix,
                handlerName, ex.Message);

            throw;
        }
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        return await Handle(request, cancellationToken, next);
    }
}