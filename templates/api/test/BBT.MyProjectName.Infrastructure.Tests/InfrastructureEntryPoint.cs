using System;
using BBT.Aether.Domain.Services;
using BBT.Aether.Testing;
using BBT.Aether.Threading;
using BBT.MyProjectName.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace BBT.MyProjectName;

public class InfrastructureEntryPoint : ModuleEntryPointBase
{
    private SqliteConnection? _sqliteConnection;

    public override void Load(IServiceCollection services)
    {
        services.AddInfrastructureModule();
        services.AddAetherAutoMapperMapper([
            typeof(MyProjectNameDomainModuleServiceCollectionExtensions), // Domain
            typeof(MyProjectNameApplicationModuleServiceCollectionExtensions) // Application
        ]);
        services.AddNetCoreDistributedCache(sc =>
        {
            sc.AddDistributedMemoryCache();
        });

        services.AddSingleton<IDataSeedService, MyProjectNameTestDataSeedService>();
        ConfigureInMemorySqlite(services);
    }

    public override void OnInitialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        scope.ServiceProvider
            .GetRequiredService<MyProjectNameDbContext>()
            .GetService<IRelationalDatabaseCreator>().CreateTables();

        SeedTestData(serviceProvider);
    }

    private void SeedTestData(IServiceProvider serviceProvider)
    {
        AsyncHelper.RunSync(async () =>
        {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider
                .GetRequiredService<IDataSeedService>()
                .SeedAsync(new SeedContext());
        });
    }

    private void ConfigureInMemorySqlite(IServiceCollection services)
    {
        _sqliteConnection = CreateDatabaseAndGetConnection(services);
        services.AddAetherDbContext<MyProjectNameDbContext>(options =>
        {
            options.UseSqlite(_sqliteConnection);
        });
    }

    private static SqliteConnection CreateDatabaseAndGetConnection(IServiceCollection services)
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        return connection;
    }
}