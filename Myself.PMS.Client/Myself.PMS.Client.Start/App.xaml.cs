using Myself.PMS.Client.BLL;
using Myself.PMS.Client.DAL;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Start.Views;
using Prism.DryIoc;
using Prism.Ioc;
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
            containerRegistry.RegisterDialog<LoginView>();


            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IFileService, FileService>();


            containerRegistry.Register<IUserAccess, UserAccess>();
            containerRegistry.Register<IFileAccess, FileAccess>();
        }
    }

}
