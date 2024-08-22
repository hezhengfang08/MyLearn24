using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace MySelf.Zero.DbMigrator
{
    internal class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IConfiguration configuration;

        public DbMigratorHostedService(IHostApplicationLifetime _hostApplicationLifetime,
    IConfiguration _configuration)
        {
            hostApplicationLifetime = _hostApplicationLifetime;
            configuration = _configuration;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
             using (var application = AbpApplicationFactory.Create<ZeroDbMigratorModule>(options =>
            {
                options.Services.ReplaceConfiguration(configuration);
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                await application.InitializeAsync();

                application
                     .ServiceProvider
                     .GetRequiredService<ZeroDbMigrationService>()
                     .Migrate();

                await application.ShutdownAsync();

                hostApplicationLifetime.StopApplication();
            }

            await Task.CompletedTask;
        }

        public  Task StopAsync(CancellationToken cancellationToken)
        {
            return  Task.CompletedTask;
        }
    }
}
