using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Zhaoxi.Forum.Application;
using Zhaoxi.Forum.EntityFrameworkCore;

namespace Zhaoxi.Forum.DbMigrator;

[DependsOn(
typeof(AbpAutofacModule),
typeof(ForumEntityFrameworkCoreModule),
typeof(ForumApplicationModule)
)]
public class ForumDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // MagicodesIE.Excel
        context.Services.AddSingleton<IImporter, ExcelImporter>();
    }
}