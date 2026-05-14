
using Prism.Ioc;
using Prism.Modularity;
using System.Reflection;

namespace Myself.PMS.Client.FeeModule
{
    public class ModuleFee : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.FeeView>();

            containerRegistry.RegisterDialog<Views.Dialogs.ModifyFeeView>();
        }
    }

}
