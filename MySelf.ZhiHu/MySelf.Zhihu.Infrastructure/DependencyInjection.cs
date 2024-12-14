using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MySelf.Zhihu.Infrastructure.Data;
using MySelf.Zhihu.Infrastructure.Data.Interceptors;
using MySelf.Zhihu.Infrastructure.Data.Repositories;
using MySelf.Zhihu.Infrastructure.Identity;
using MySelf.Zhihu.SharedKernel.Repositoy;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System.Text;

namespace MySelf.Zhihu.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureEfCore(services, configuration);

            ConfigureIdentity(services, configuration);

            return services;
        }
        private static void ConfigureEfCore(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<AppDbInitializer>();

            services.AddScoped(typeof(IReadRepository<>), typeof(EfReadRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IDataQueryService, DataQueryService>();
        }

        private static void ConfigureIdentity(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentityCore<IdentityUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IdentityService>();

            // 从配置文件中读取JwtSettings，并注入到容器中
            var configurationSection = configuration.GetSection("JwtSettings");
            var jwtSettings = configurationSection.Get<JwtSettings>();
            if (jwtSettings is null) throw new NullReferenceException(nameof(jwtSettings));
            services.Configure<JwtSettings>(configurationSection);

            ConfigureAuthentication(services, jwtSettings);
        }

        public static void ConfigureAuthentication(IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret)
                        )
                    };
                });
        }

        }
}
