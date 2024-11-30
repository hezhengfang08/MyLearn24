using Microsoft.EntityFrameworkCore;
using Myself.SmartParing.IService;
using Myself.SmartParking.Base;
using Myself.SmartParking.Service;
using Myself.SmartParking.Views;
using Myself.SmartParking.ORM;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Myself.SmartParking
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
        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);

            Container.Resolve<IRegionManager>().RegisterViewWithRegion("MainRegion", "DashboardView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<LoginView>();
            containerRegistry.RegisterDialogWindow<DialogWindowEx>();
           


            // 注册相关的实体
            containerRegistry.Register<DbContext, MyselfDbContext>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IMenuService, MenuService>();
            containerRegistry.RegisterForNavigation<Views.Pages.DashboardView>();
        }
    }

}
