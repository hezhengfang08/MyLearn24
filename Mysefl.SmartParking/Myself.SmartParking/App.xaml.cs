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
            containerRegistry.RegisterScoped<DbContext, MyselfDbContext>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IMenuService, MenuService>();
            containerRegistry.Register<IRoleService, RoleService>();
            containerRegistry.Register<IDeviceService, DeviceService>();
            containerRegistry.Register<IAutoService, AutoService>();
            containerRegistry.Register<IRechargeService, RechargeSerivce>();

            containerRegistry.RegisterForNavigation<Views.Pages.DashboardView>();
            containerRegistry.RegisterForNavigation<Views.Pages.MenuManagementView>();
            containerRegistry.RegisterForNavigation<Views.Pages.UserManagementView>();
            containerRegistry.RegisterForNavigation<Views.Pages.RoleView>();
            containerRegistry.RegisterForNavigation<Views.Pages.DeviceView>();
            containerRegistry.RegisterForNavigation<Views.Pages.AutoView>();
            containerRegistry.RegisterForNavigation<Views.Pages.RechargeView>();
            containerRegistry.RegisterForNavigation<Views.Pages.MonitorView>();

            containerRegistry.RegisterDialog<Views.Pages.Dialogs.ModifyMenuView>();
            containerRegistry.RegisterDialog<Views.Pages.Dialogs.ModifyUserView>();
            containerRegistry.RegisterDialog<Views.Pages.Dialogs.ModifyPasswordView>();
            containerRegistry.RegisterDialog<Views.Pages.Dialogs.ModifyRoleView>();
            containerRegistry.RegisterDialog<Views.Pages.Dialogs.SelectUserView>();
            containerRegistry.RegisterDialog<Views.Pages.Dialogs.ModifyDeviceView>();
            containerRegistry.RegisterDialog<Views.Pages.Dialogs.ModifyAutoView>();
            containerRegistry.RegisterDialog<Views.Pages.Dialogs.ModifyRechargeView>();

        }
    }

}
