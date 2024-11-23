
using MySelf.PMS.Client.BLL;
using MySelf.PMS.Client.DAL;
using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.IDAL;
using MySelf.PMS.Client.Start.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace MySelf.PMS.Client.Start
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


            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IFileService, FileService>();
            containerRegistry.Register<IMenuService, MenuService>();


            containerRegistry.Register<IUserAccess, UserAccess>();
            containerRegistry.Register<IFileAccess, FileAccess>();
            containerRegistry.Register<IMenuAccess, MenuAccess>();
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
