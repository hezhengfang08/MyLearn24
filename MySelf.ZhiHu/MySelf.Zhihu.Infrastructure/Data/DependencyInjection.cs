using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.Zhihu.Infrastructure.Data.Interceptors;
using MySelf.Zhihu.Infrastructure.Identity;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data
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
            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IIdentityService, IdentityService>();
            // 从配置文件中读取JwtSettings，并注入到容器中
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            return services;
        }

    }
}
