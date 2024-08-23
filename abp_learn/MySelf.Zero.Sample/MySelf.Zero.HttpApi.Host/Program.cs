using Serilog;
using Serilog.Events;
using MySelf.Zero.HttpApi.Host;

Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("logs/logs.log"))
#if DEBUG
            .WriteTo.Async(c => c.Console())
#endif
            .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddAppSettingsSecretsJson()
    .UseAutofac()        //使用注入
    .UseSerilog();       //使用日志
// Add services to the container.
await builder.AddApplicationAsync<ZeroHttpApiHostModule>();

var app = builder.Build();
// Configure the HTTP request pipeline.
await app.InitializeApplicationAsync();
await app.RunAsync();