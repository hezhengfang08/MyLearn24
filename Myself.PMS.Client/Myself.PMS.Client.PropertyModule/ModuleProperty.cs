
using Prism.Ioc;
using Prism.Modularity;
using System.Reflection;

namespace Myself.PMS.Client.PropertyModule
{
    public class ModuleProperty: IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.OwnerView>();

            containerRegistry.RegisterDialog<Views.Dialogs.ModifyOwnerView>();
        }
    }

}
