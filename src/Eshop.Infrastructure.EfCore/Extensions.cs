using System.Reflection;
using Eshop.Core.Domain.Interfaces;
using Eshop.Core.Domain.Repository;
using Eshop.Infrastructure.EfCore.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Infrastructure.EfCore
{
    public static class Extensions
    {
        public static IServiceCollection AddPostgresDbContext<TDbContext>(this IServiceCollection services,
            string connString, Action<DbContextOptionsBuilder>? doMoreDbContextOptionsConfigure = null,
            Action<IServiceCollection>? doMoreActions = null)
            where TDbContext : DbContext, IDbFacadeResolver, IDomainEventContext
        {
            services.AddDbContext<TDbContext>(options =>
            {
                options.UseNpgsql(connString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }).UseSnakeCaseNamingConvention();

                doMoreDbContextOptionsConfigure?.Invoke(options);
            });

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<TDbContext>()!);
            services.AddScoped<IDomainEventContext>(provider => provider.GetService<TDbContext>()!);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>));

            services.AddHostedService<DbContextMigratorHostedService>();

            doMoreActions?.Invoke(services);

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, Type repoType)
        {
            var repoAssembly = repoType.Assembly;

            var repoInterfaces = repoAssembly.GetTypes()
                .Where(type =>
                    type.IsInterface && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IRepository<>));

            foreach (var repoInterface in repoInterfaces)
            {
                var repoImplementations = repoAssembly.GetTypes()
                    .Where(type => !type.IsAbstract && repoInterface.IsAssignableFrom(type));

                foreach (var repoImplementation in repoImplementations)
                {
                    services.AddScoped(repoInterface, repoImplementation);
                }
            }

            return services;
        }

        public static void MigrateDataFromScript(this MigrationBuilder migrationBuilder)
        {
            var assembly = Assembly.GetCallingAssembly();
            var files = assembly.GetManifestResourceNames();
            var filePrefix = $"{assembly.GetName().Name}.Data.Scripts."; //IMPORTANT

            foreach (var file in files
                         .Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                         .Select(f => new { PhysicalFile = f, LogicalFile = f.Replace(filePrefix, string.Empty) })
                         .OrderBy(f => f.LogicalFile))
            {
                using var stream = assembly.GetManifestResourceStream(file.PhysicalFile);
                using var reader = new StreamReader(stream!);
                var command = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(command))
                    continue;

                migrationBuilder.Sql(command);
            }
        }
    }
}