using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

string environmentName =
    Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
    ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
    ?? "Development";

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
    .Build();

var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((options) =>
    {
        options.Sources.Clear();
        options.AddJsonFile("appsettings.json", optional: false);
        options.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
    })
    .ConfigureServices(services =>
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        services.AddDbContext<NotifyXDbContext>(options =>
        {
            options.UseNpgsql(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("default"),
                npgsqlOptions => npgsqlOptions.MigrationsAssembly("DbMigrations"));
        });
    });


IHost host = hostBuilder.Build();
IServiceProvider serviceProvider = host.Services;

using IServiceScope scope = serviceProvider.CreateScope();
var databaseContext = scope.ServiceProvider.GetRequiredService<NotifyXDbContext>();
await databaseContext.Database.MigrateAsync();
Console.WriteLine("Press any key to exit");
Console.ReadKey();