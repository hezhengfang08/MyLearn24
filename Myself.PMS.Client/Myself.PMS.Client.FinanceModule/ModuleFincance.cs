
using Prism.Ioc;
using Prism.Modularity;

namespace Myself.PMS.Client.FinanceModule
{
    public class ModuleFincance : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.IEDetailView>();

            containerRegistry.RegisterDialog<Views.Dialogs.ModfyIEDetailView>();
        }
    }

}
