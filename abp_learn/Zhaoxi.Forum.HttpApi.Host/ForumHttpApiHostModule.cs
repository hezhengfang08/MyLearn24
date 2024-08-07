using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Zhaoxi.Forum.Application;
using Zhaoxi.Forum.EntityFrameworkCore;

namespace Zhaoxi.Forum.HttpApi.Host;

[DependsOn(typeof(AbpAspNetCoreMvcModule), typeof(AbpAutofacModule))]
[DependsOn(typeof(ForumApplicationModule),
    typeof(ForumEntityFrameworkCoreModule)
)]
public class ForumHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        var configuration = services.GetConfiguration();

        // 跨域
        context.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        // 自动生成控制器
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(ForumApplicationModule).Assembly);
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