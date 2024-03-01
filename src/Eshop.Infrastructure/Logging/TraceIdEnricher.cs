using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Eshop.Infrastructure.Logging
{
    internal class TraceIdEnricher(string traceIdName, IHttpContextAccessor httpContextAccessor)
        : ILogEventEnricher
    {
        private const string DefaultPropertyName = "TraceId";

        public TraceIdEnricher(IHttpContextAccessor httpContextAccessor)
            : this(DefaultPropertyName, httpContextAccessor)
        {
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var traceId = Activity.Current?.TraceId.ToString() ?? httpContextAccessor?.HttpContext?.TraceIdentifier;
            var versionProperty = propertyFactory.CreateProperty(traceIdName, traceId);
            logEvent.AddPropertyIfAbsent(versionProperty);
        }
    }
}