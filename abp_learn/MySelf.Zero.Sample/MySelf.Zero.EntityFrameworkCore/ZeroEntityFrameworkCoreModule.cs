using Microsoft.Extensions.DependencyInjection;
using MySelf.Zero.Domain;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace MySelf.Zero.EntityFrameworkCore;

[DependsOn(typeof(ZeroDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule))]
public class ZeroEntityFrameworkCoreModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ZeroDbContext>(option =>
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
