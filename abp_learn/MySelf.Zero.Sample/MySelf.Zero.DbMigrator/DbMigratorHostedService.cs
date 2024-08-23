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
using Volo.Abp.Data;

namespace MySelf.Zero.DbMigrator
{
    internal class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IConfiguration configuration;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime,
            IConfiguration configuration)
        {
            this.hostApplicationLifetime = hostApplicationLifetime;
            this.configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<ZeroDbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.ReplaceConfiguration(configuration);
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                await application.InitializeAsync();
                ////这里要执行两次 有时要删除 __efmigrationshistory 表的相关数据
                //application
                //     .ServiceProvider
                //     .GetRequiredService<ZeroDbMigrationService>()
                //     .Migrate();
                //  下面初始化 种子数据的
                await application
                    .ServiceProvider
                    .GetRequiredService<DefaultDataSeedContributor>()
                    .SeedAsync(new DataSeedContext());

                await application.ShutdownAsync();

                hostApplicationLifetime.StopApplication();
            }

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
