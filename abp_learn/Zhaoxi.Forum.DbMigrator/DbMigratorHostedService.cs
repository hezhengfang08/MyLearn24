using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;
using Volo.Abp.Data;

namespace Zhaoxi.Forum.DbMigrator;

public class DbMigratorHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IConfiguration _configuration;

    public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime,
IConfiguration configuration)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var application = AbpApplicationFactory.Create<ForumDbMigratorModule>(options =>
        {
            options.UseAutofac();
            options.Services.ReplaceConfiguration(_configuration);
            options.Services.AddLogging(c => c.AddSerilog());
        }))
        {
            await application.InitializeAsync();

            //application
            //     .ServiceProvider
            //     .GetRequiredService<ForumDbMigrationService>()
            //    .Migrate();
            await application
                .ServiceProvider
                .GetRequiredService<DefaultDataSeedContributor>()
                .SeedAsync(new DataSeedContext());


            await application.ShutdownAsync();

            _hostApplicationLifetime.StopApplication();
        }

        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}