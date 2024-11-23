
using Prism.Ioc;
using Prism.Modularity;
namespace MySelf.PMS.Client.MainModule
{
    public class ModuleMain : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.DashboardView>();
            containerRegistry.RegisterForNavigation<Views.PageView>();

            containerRegistry.RegisterDialog<Views.Dialogs.ModifyPasswordView>();
        }
    }

}
