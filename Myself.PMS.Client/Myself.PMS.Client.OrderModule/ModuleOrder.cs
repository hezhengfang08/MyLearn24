
using Prism.Ioc;
using Prism.Modularity;

namespace Myself.PMS.Client.OrderModule
{
    public class ModuleOrder : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.OrderView>();
        }
    }

}
