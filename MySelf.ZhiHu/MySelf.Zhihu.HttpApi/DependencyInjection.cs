using MySelf.Zhihu.HttpApi.Services;
using MySelf.Zhihu.UseCases.Common.Interfaces;

namespace MySelf.Zhihu.HttpApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            services.AddExceptionHandler<UseCaseExceptionHandler>();

            return services;
        }
    }
}
