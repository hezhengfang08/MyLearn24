using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MySelf.Zero.Domain;

public class ZeroDomainModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
       
    }
}
