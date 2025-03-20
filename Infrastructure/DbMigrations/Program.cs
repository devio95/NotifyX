using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder(args)

    .ConfigureServices(services =>
    {
        services.AddDatabase();
    });


IHost host = hostBuilder.Build();
IServiceProvider serviceProvider = host.Services;

using IServiceScope scope = serviceProvider.CreateScope();
var databaseContext = scope.ServiceProvider.GetRequiredService<NotifyXDbContext>();
await databaseContext.Database.MigrateAsync();
Console.WriteLine("Press any key to exit");
Console.ReadKey();