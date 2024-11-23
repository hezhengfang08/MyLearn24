using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.Extensions.DependencyInjection;
using MySelf.Zero.Application;
using MySelf.Zero.Application.Contracts.Category;
using MySelf.Zero.Domain.Repositories;
using MySelf.Zero.EntityFrameworkCore;
using MySelf.Zero.EntityFrameworkCore.Repositories;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MySelf.Zero.DbMigrator
{
    //auto 依赖注入
    [DependsOn(
typeof(AbpAutofacModule),
typeof(ZeroEntityFrameworkCoreModule),
typeof(ZeroApplicationModule)
)]
    public class ZeroDbMigratorModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpContextAccessor();
            context.Services.AddSingleton<IImporter, ExcelImporter>();
          
        }
        
    }
}
