using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.Start.Models;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

using System.Windows;

namespace MySelf.PMS.Client.Start.ViewModels
{
    public class MainViewModel
    {
        public List<MenuModel> Menus { get; set; } =
            new List<MenuModel>();
        private Entities.MenuEntity[] menus;


        public DelegateCommand WorkbenchCommand { get; set; }
        public DelegateCommand<string> PageSwitchCommand { get; set; }

        Entities.EmployeeEntity _currentUser;
        IRegionManager _regionManager;
        public MainViewModel(
            IDialogService dialogService,
            IMenuService menuService,
            IRegionManager regionManager)
        {
            _regionManager = regionManager;

            // 打开登录弹窗
            dialogService.ShowDialog("LoginView", result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    //Application.Current.Shutdown();
                    System.Environment.Exit(0);
                }
                _currentUser = result.Parameters.GetValue<Entities.EmployeeEntity>("user");
            });
            PageSwitchCommand = new DelegateCommand<string>(DoPageSwitch);
            WorkbenchCommand = new DelegateCommand(ShowWorkbench);

            // 获取出所需要的第一级菜单信息
            menus = menuService.GetAllMenus().ToArray();
            foreach (var me in menus.Where(m => m.ParentId == "-1"))
            {
                Menus.Add(new MenuModel { MenuId = me.MenuId, MenuHeader = me.MenuHeader });
            }
            if (menus.Count() > 0)
                Menus[0].IsSelected = true;

        }

        private void ShowWorkbench()
        {
            NavigationParameters nps = new NavigationParameters();
            nps.Add("user", _currentUser);
            // 页面Loaded时触发
            _regionManager.RequestNavigate("MainRegion", "DashboardView", nps);
        }

        private void DoPageSwitch(string id)
        {
            // IRegionManager
            // 默认需要导航Dashboard页面显示
            if (id == "0")
                ShowWorkbench();
            else
            {
                // 这里根据id进行子菜单的获取
                var ms = menus.Where(m => m.ParentId == id).ToList();
                NavigationParameters nps = new NavigationParameters();
                nps.Add("menu", ms);
                // 页面Loaded时触发
                _regionManager.RequestNavigate("MainRegion", "PageView", nps);
            }
        }
    }
}
