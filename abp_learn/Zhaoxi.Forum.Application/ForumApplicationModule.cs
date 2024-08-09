using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Zhaoxi.Forum.Application.Contracts;
using Zhaoxi.Forum.Application.Contracts.Auth;
using Zhaoxi.Forum.Domain;

namespace Zhaoxi.Forum.Application;

/// <summary>
/// 项目模块依赖、组件依赖
/// </summary>
[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(ForumApplicationContractsModule),
    typeof(ForumDomainModule)
    )]
public class ForumApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        //注入
        services.AddScoped<IJwtAppService, JwtAppService>();
        // 添加ObjectMapper注入
        services.AddAutoMapperObjectMapper<ForumApplicationModule>();

        // Abp AutoMapper设置
        Configure<AbpAutoMapperOptions>(config =>
        {
            // 添加对应依赖关系Profile
            config.AddMaps<ForumApplicationAutoMapperProfile>();
        });
    }
}