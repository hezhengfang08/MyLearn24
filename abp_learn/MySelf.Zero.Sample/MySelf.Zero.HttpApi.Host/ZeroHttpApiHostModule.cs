using MySelf.Zero.Application;
using MySelf.Zero.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MySelf.Zero.HttpApi.Host
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule), typeof(AbpAutofacModule))]
    [DependsOn(typeof(ZeroApplicationModule),
    typeof(ZeroEntityFrameworkCoreModule)
)]
    public class ZeroHttpApiHostModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services; //服务
            var configuration = services.GetConfiguration();//配置

            // 跨域
            context.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    //允许所有的请求
                    builder.AllowAnyOrigin()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // 自动生成控制器
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {  
                //把Application 服务自动生成Api接口
                options.ConventionalControllers.Create(typeof(ZeroApplicationModule).Assembly);
            });

            // swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "ForumApi",
                    Version = "v0.1"
                });
                options.DocInclusionPredicate((docName, predicate) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ForumApi");
            });

            app.UseRouting();
            app.UseConfiguredEndpoints();
        }
    }
}
