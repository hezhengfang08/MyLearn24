using Microsoft.Extensions.DependencyInjection;
using MySelf.Zhihu.HotService.Core;
using MySelf.Zhihu.HotService.Data;

namespace MySelf.Zhihu.HotService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHotService(this IServiceCollection services)
        {
            services.AddSingleton<QuestionStatManager>();

            services.AddTransient<QuestionStatQuery>();
            services.AddTransient<HotRankManager>();

            return services;
        }
    }

}
