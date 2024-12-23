using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MySelf.Zhihu.Feed.UseCases.Consumers;
using System.Reflection;

namespace MySelf.Zhihu.Feed.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCaseServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }

        public static void AddUseCaseConsumers(this IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<FeedCreatedConsumer>();
        }
    }

}
