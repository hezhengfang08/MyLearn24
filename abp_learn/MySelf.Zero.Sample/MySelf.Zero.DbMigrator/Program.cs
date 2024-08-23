using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MySelf.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySelf.Zero.DbMigrator;
using MySelf.Zero.DbMigrator;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
        .MinimumLevel.Override("MySelf.Zero", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("MySelf.Zero", LogEventLevel.Information)
#endif
        .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("logs/logs.log"))
    .WriteTo.Async(c => c.Console())
    .CreateLogger();

await CreateHostBuilder(args).RunConsoleAsync();


static IHostBuilder CreateHostBuilder(string[] args) =>
   Host.CreateDefaultBuilder(args)
   .AddAppSettingsSecretsJson()
   .ConfigureLogging((context, logging) => logging.ClearProviders())
   .ConfigureServices((hostContext, services) =>
   {
       var configuration = hostContext.Configuration;
       var connectionString = configuration.GetConnectionString("Default");

       services.AddDbContext<ZeroDbContext>(
           options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
           action =>
           {
               action.MigrationsAssembly("MySelf.Zero.DbMigrator");
           }));

       services.AddHostedService<DbMigratorHostedService>();
   });
