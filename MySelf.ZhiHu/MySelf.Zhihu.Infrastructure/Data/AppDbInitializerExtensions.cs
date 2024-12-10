using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data
{
    public static class AppDbInitializerExtensions
    {
        public static async Task InitialzeDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<AppDbInitializer>();

            await initializer.InitializeAsync();

            await initializer.SeedAsync();
        }
    }
}
