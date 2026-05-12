
using Prism.Ioc;
using Prism.Modularity;

namespace Myself.PMS.Client.SystemModule
{
    public class ModuleSystem:IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }  
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册系统模块的视图和视图模型
            containerRegistry.RegisterForNavigation<Views.MenuView>();

            containerRegistry.RegisterDialog<Views.Dialogs.ModifyMenuView>();

            containerRegistry.RegisterDialog<Views.UploadView>();
            containerRegistry.RegisterDialog<Views.UserView>();
        }
    }

}
