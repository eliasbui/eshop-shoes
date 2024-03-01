using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Eshop.Infrastructure.EfCore.Internal;

public class DbContextMigratorHostedService(
    IServiceProvider serviceProvider,
    ILogger<DbContextMigratorHostedService> logger)
    : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<DbContextMigratorHostedService> _logger = logger;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var policy = CreatePolicy(3, _logger, nameof(DbContextMigratorHostedService));
        await policy.ExecuteAsync(async () =>
        {
            using var scope = _serviceProvider.CreateScope();
            var dbFacadeResolver = scope.ServiceProvider.GetRequiredService<IDbFacadeResolver>();

            if (!await dbFacadeResolver.Database.CanConnectAsync(cancellationToken))
            {
                throw new Exception("Couldn't connect database.");
            }

            await dbFacadeResolver.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("Done migration database schema.");
        });
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static AsyncRetryPolicy CreatePolicy(int retries, ILogger logger, string prefix)
    {
        return Policy.Handle<Exception>().WaitAndRetryAsync(
            retries,
            retry => TimeSpan.FromSeconds(5),
            (exception, timeSpan, retry, ctx) =>
            {
                logger.LogWarning(exception,
                    "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}",
                    prefix, exception.GetType().Name, exception.Message, retry, retries);
            }
        );
    }
}