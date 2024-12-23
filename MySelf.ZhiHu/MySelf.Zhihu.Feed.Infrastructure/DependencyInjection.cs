using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.Zhihu.Feed.Infrastructure.Data.Contexts;
using MySelf.Zhihu.Feed.Infrastructure.Data.Queries;
using MySelf.Zhihu.Feed.Infrastructure.Data.Repositories;
using MySelf.Zhihu.Feed.Infrastructure.Data;
using MySelf.Zhihu.SharedKernel.Repositoy;
using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.Feed.UseCases.Common.Interfaces;
using MassTransit;
using MySelf.Zhihu.Feed.UseCases;

namespace MySelf.Zhihu.Feed.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            ConfigureEfCore(services, configuration);

            ConfigureRabbitMq(services, configuration);

            return services;
        }

        private static void ConfigureEfCore(IServiceCollection services, IConfiguration configuration)
        {
            var appDbConnection = configuration.GetConnectionString("AppDbConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(appDbConnection, ServerVersion.AutoDetect(appDbConnection));
            });

            var feedDbConnection = configuration.GetConnectionString("FeedDbConnection");

            services.AddDbContext<FeedDbContext>(options =>
            {
                options.UseMySql(feedDbConnection, ServerVersion.AutoDetect(feedDbConnection));
            });

            services.AddScoped(typeof(IReadRepository<>), typeof(EfReadRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IFollowUserQueryService, FollowUserQueryService>();
            services.AddScoped<IDataQueryService, DataQueryService>();
        }

        private static void ConfigureRabbitMq(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.AddUseCaseConsumers();
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    // 需要安装延迟交换机插件
                    // https://github.com/rabbitmq/rabbitmq-delayed-message-exchange
                    // cfg.UseDelayedRedelivery(r => r.Interval(5, TimeSpan.FromMinutes(5)));

                    cfg.Host(configuration.GetConnectionString("RabbitMqConnection"));
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(10)));
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }

}
