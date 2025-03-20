﻿using Microsoft.EntityFrameworkCore;
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
            options.UseNpgsql(dbConfig.GetConnectionString("Default"),
                npgsqlOptions => npgsqlOptions.MigrationsAssembly("DbMigrations"));
        });
    }

    private static IConfiguration LoadDbSettings()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("DbSettings.json")
            .Build();
    }
}