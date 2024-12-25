using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using MySelf.Zhihu.Infrastructure.Cache;
using MySelf.Zhihu.Infrastructure.Data;
using MySelf.Zhihu.Infrastructure.Data.Interceptors;
using MySelf.Zhihu.Infrastructure.Data.Repositories;
using MySelf.Zhihu.Infrastructure.Identity;
using MySelf.Zhihu.Infrastructure.Quartz;
using MySelf.Zhihu.SharedKernel.Repositoy;
using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;
using MySelf.Zhihu.UseCases.Questions.Jobs;
using Quartz;
using StackExchange.Redis;
using System.Text;
using ZiggyCreatures.Caching.Fusion.Backplane.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion;
using MassTransit;
using MySelf.Zhihu.Infrastructure.MassTransit;
using MassTransit.RabbitMqTransport.Configuration;


namespace MySelf.Zhihu.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureEfCore(services, configuration);

            ConfigureIdentity(services, configuration);

            ConfigureQuartz(services, configuration);

            ConfigureCache(services, configuration);

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
        private static void ConfigureQuartz(IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartzService(configuration);
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
        private static void ConfigureCache(IServiceCollection services, IConfiguration configuration)
        {
            var redisConn = configuration.GetConnectionString("RedisConnection");
            if (redisConn != null)
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConn));

            services.AddStackExchangeRedisCache(options => options.Configuration = redisConn);
            services.AddFusionCache()
                .WithOptions(options =>
                {
                    options.DefaultEntryOptions = new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(1) };
                })
                .WithSystemTextJsonSerializer()
                .WithDistributedCache(provider => provider.GetRequiredService<IDistributedCache>())
                .WithBackplane(new RedisBackplane(new RedisBackplaneOptions
                {
                    Configuration = redisConn
                }));

            services.AddSingleton(typeof(ICacheService<>), typeof(CacheService<>));
        }
        private static void ConfigureRabbitMq(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMqConnection"));
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
                });
            });

            services.AddScoped<IMessageBusService, MessageBusService>();
        }
    }
}
