using MySelf.Zhihu.HttpApi.Services;
using MySelf.Zhihu.UseCases.Common.Interfaces;

namespace MySelf.Zhihu.HttpApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
