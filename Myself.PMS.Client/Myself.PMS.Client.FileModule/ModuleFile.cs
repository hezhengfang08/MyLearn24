
using Prism.Ioc;
using Prism.Modularity;

namespace Myself.PMS.Client.FileModule
{
    public class ModuleFile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.ContractView>();

            containerRegistry.RegisterDialog<Views.Dialogs.ModifyContractView>();
            containerRegistry.RegisterDialog<Views.Dialogs.ExecuteView>();
        }
    }
}
