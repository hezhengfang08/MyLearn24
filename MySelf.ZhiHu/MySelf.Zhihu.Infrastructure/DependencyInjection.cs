using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.Zhihu.Infrastructure.Data;
using MySelf.Zhihu.Infrastructure.Data.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectStr = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<ISaveChangesInterceptor, AuditEntityInterceptor>();

            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options.UseMySql(connectStr, ServerVersion.AutoDetect(connectStr));
            });
            services.AddScoped<AppDbInitializer>();
            services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

            return services;
        }

    }
}
