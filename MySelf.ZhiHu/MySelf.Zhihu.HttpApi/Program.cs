using MySelf.Zhihu.HttpApi;
using MySelf.Zhihu.Infrastructure;
using MySelf.Zhihu.UseCases;
using MySelf.Zhihu.Core;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddUseCaseServices();

builder.Services.AddCoreServices();

builder.Services.AddWebServices(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAny");

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(_ => { });

app.MapControllers();

app.Run();
