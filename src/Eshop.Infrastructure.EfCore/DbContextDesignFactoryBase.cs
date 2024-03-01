using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Eshop.Infrastructure.EfCore;

public abstract class DbContextDesignFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : DbContext
{
    public TDbContext CreateDbContext(string[] args)
    {
        // var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
        //     ?.GetConnectionString(""postgres"");
        var connString = "";
        Console.WriteLine($"Connection String: {connString}");

        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
            .UseNpgsql(
                connString ?? throw new InvalidOperationException(),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                }
            ).UseSnakeCaseNamingConvention();

        Console.WriteLine(connString);
        return ((TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options)!)!;
    }
}