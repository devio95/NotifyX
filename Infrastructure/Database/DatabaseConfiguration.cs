using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database;
public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        IConfiguration dbConfig = LoadDbSettings();

        return services.AddDbContext<NotifyXDbContext>(options =>
        {
            options.UseNpgsql(dbConfig.GetConnectionString("DefaultConnection"),
                npgsqlOptions => npgsqlOptions.MigrationsAssembly("DbMigrations"));
        });
    }

    private static IConfiguration LoadDbSettings()
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("DbSettings.json")
            .Build();
    }
}