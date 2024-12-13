using Microsoft.Extensions.DependencyInjection;
using MySelf.Zhihu.Core.Interfaces;
using MySelf.Zhihu.Core.QuestionAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IFollowQuestionService, FollowQuestionService>();
            return services;
        }
    }
}
