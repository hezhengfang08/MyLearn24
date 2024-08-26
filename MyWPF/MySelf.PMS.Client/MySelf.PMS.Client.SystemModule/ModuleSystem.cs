using Prism.Ioc;
using Prism.Modularity;
namespace MySelf.PMS.Client.SystemModule
{
    public class ModuleSystem : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.MenuView>();
            containerRegistry.RegisterForNavigation<Views.UploadView>();
            containerRegistry.RegisterDialog<Views.Dialogs.ModifyMenuView>();
        }
    }
}
