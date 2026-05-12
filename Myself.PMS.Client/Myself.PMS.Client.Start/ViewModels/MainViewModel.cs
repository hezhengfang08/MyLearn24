using Myself.PMS.Client.Common;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.Start.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Myself.PMS.Client.Start.ViewModels
{
    public class MainViewModel: BindableBase
    {
        private int _viewBlur;

        public int ViewBlur
        {
            get { return _viewBlur; }
            set { SetProperty(ref _viewBlur, value); }
        }
        private bool _showLoading;

        public bool ShowLoading
        {
            get { return _showLoading; }
            set
            {
                SetProperty<bool>(ref _showLoading, value, () =>
                {
                    ViewBlur = value ? 5 : 0;
                });
            }
        }
        private string _loadingTip;

        public string LoadingTip
        {
            get { return _loadingTip; }
            set { SetProperty<string>(ref _loadingTip, value); }
        }
        public List<MenuModel> Menus { get; set; } =
           new List<MenuModel>();
        private Entities.MenuEntity[] menus;

        Entities.EmployeeEntity _currentUser;
        IRegionManager _regionManager;
        IMenuService _menuService;
        public MainViewModel(
            IDialogService dialogService
            , IRegionManager regionManager
            , IMenuService menuService
            , IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _menuService = menuService; 
            // 打开登录弹窗
            // 打开登录弹窗
            dialogService.ShowDialog("LoginView", result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    Application.Current.Shutdown();
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
            {
                Menus[0].IsSelected = true;
            }
            eventAggregator.GetEvent<LoadingEvent>()
               .Subscribe(tip =>
               {
                   // 显示或隐藏Loading动画
                   ShowLoading = !ShowLoading;
                   this.LoadingTip = tip;
               });
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
        public DelegateCommand WorkbenchCommand { get; set; }
        public DelegateCommand<string> PageSwitchCommand { get; set; }
    }
}
