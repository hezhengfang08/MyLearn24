﻿
using Microsoft.Extensions.DependencyInjection;
using MySelf.Zhihu.UseCases.Common.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
 using FluentValidation;
using MySelf.Zhihu.HotService;
using MySelf.Zhihu.UseCases.Questions.Jobs;

namespace MySelf.Zhihu.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCaseServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Core.DependencyInjection))!);
                cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(HotService.DependencyInjection))!);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            services.AddSingleton<QuestionViewCountService>();
            services.AddHotService();
            return services;
        }
    }
}
