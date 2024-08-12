
using MySelf.PMS.Client.BLL;
using MySelf.PMS.Client.DAL;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.IDAL;
using MySelf.PMS.Client.Start.Views;
using Prism.DryIoc;
using Prism.Ioc;

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
            containerRegistry.RegisterDialog<LoginView>();


            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IFileService, FileService>();


            containerRegistry.Register<IUserAccess, UserAccess>();
            containerRegistry.Register<IFileAccess, FileAccess>();
        }
    }

}
