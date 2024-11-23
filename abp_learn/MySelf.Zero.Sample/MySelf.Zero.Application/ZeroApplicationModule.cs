using Microsoft.Extensions.DependencyInjection;
using MySelf.Zero.Application.Contracts;
using MySelf.Zero.Application.Contracts.Auth;
using MySelf.Zero.Application.Contracts.Category;
using MySelf.Zero.Application.Contracts.Topic;
using MySelf.Zero.Domain;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MySelf.Zero.Application;
[DependsOn(typeof(AbpAutoMapperModule),
    typeof(ZeroApplicationContractsModule),
    typeof(ZeroDomainModule))]
public class ZeroApplicationModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        // jwtAppService
        services.AddScoped<IJwtAppService, JwtAppService>();
        // 添加ObjectMapper注入
        services.AddAutoMapperObjectMapper<ZeroApplicationModule>();

        // Abp AutoMapper设置
        Configure<AbpAutoMapperOptions>(config =>
        {
            // 添加对应依赖关系Profile
            config.AddMaps<ZeroApplicationAutoMapperProfile>();
            
        });
    }

}
