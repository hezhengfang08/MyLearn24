using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;
using Zhaoxi.Forum.Domain;
using Zhaoxi.Forum.Domain.Entities;

namespace Zhaoxi.Forum.EntityFrameworkCore;

[DependsOn(typeof(ForumDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule))]
public class ForumEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ForumDbContext>(option =>
        {
            // 如果只是用了 entity， 请将 includeAllEntities 设置为 true 否则会提示异常，导致所属仓储无法注入
            option.AddDefaultRepositories(true);
        });

        Configure<AbpDbContextOptions>(option =>
        {
            option.UseMySQL();
        });
    }
}