using Myself.PMS.Client.BLL;
using Myself.PMS.Client.DAL;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Start.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Myself.PMS.Client.Start
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<GlobalValues>();

            containerRegistry.RegisterDialogWindow<DialogWindow>();
            containerRegistry.RegisterDialog<LoginView>();
            containerRegistry.RegisterDialog<LoginView>();


            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IFileService, FileService>();
            containerRegistry.Register<IMenuService, MenuService>();
            containerRegistry.Register<IRoleService, RoleService>();
            containerRegistry.Register<IBaseInfoService, BaseInfoService>();
            containerRegistry.Register<IOwnerService, OwnerService>();

            containerRegistry.Register<IUserAccess, UserAccess>();
            containerRegistry.Register<IFileAccess, FileAccess>();
            containerRegistry.Register<IMenuAccess, MenuAccess>();
            containerRegistry.Register<IRoleAccess, RoleAccess>();
            containerRegistry.Register<IBaseInfoAccess, BaseInfoAccess>();
            containerRegistry.Register<IOwnerAccess, OwnerAccess>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog
            {
                ModulePath = Environment.CurrentDirectory + "\\Modules"
            };
            //return base.CreateModuleCatalog();
        }
    }

}
