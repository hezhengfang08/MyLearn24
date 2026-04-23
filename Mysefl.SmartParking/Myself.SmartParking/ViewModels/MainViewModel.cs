using Myself.SmartParing.IService;
using Myself.SmartParking.Base;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using Myself.SmartParking.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Myself.SmartParking.ViewModels
{
    public class MainViewModel : BindableBase
    {
        List<Entities.SysMenu> sysMenuList;
        public UserModel CurrentUser { get; set; } = new UserModel();
        IRegionManager _regionManager;
        private readonly IMenuService _menuService;
        public MainViewModel(
            IDialogService dialogService
            , IMenuService menuService
            , IRegionManager regionManager
            ,IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _menuService = menuService;
            // 打开登录窗口
            dialogService.ShowDialog("LoginView", result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    System.Environment.Exit(0);
                }
                else 
                {
                    // 记录当前登录用户信息
                    var su = result.Parameters.GetValue<SysUser>("user");
                    CurrentUser.UserId = su.UserId;
                    CurrentUser.UserName = su.UserName;
                    CurrentUser.RealName = su.RealName;
                    CurrentUser.Password = su.Password;
                    CurrentUser.UserIcon = "pack://siteoforigin:,,,/Avatars/" + su.UserIcon;
                    CurrentUser.Gender = su.Gender;
                    CurrentUser.Address = su.Address;
                    CurrentUser.Age = su.Age;
                    CurrentUser.Status = su.Status;
                    CurrentUser.Phone = su.Phone;
                }
            });
            // 当前窗口要做的事
            OpenViewCommand = new DelegateCommand<MenuItemModel>(DoOpenView);
            CloseCommand = new DelegateCommand(() =>
            {
                System.Environment.Exit(0);
            });
            // 加载菜单
            eventAggregator.GetEvent<RefreshMenuEvent>()
               .Subscribe(() =>
               {
                   LoadMenus();
               });
            // 加载菜单
            LoadMenus();
           
            FillMenus(Menus, 0);
        }
        private void LoadMenus()
        {
            Menus.Clear();
            // 获取所有菜单
            sysMenuList = _menuService.GetMenuList().ToList();
            // 树状填充
            FillMenus(Menus, 0);
        }
        #region 菜单相关功能

        public DelegateCommand<MenuItemModel> OpenViewCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        /// <summary>
        /// 菜单集合
        /// </summary>
        private ObservableCollection<MenuItemModel> _menus =
          new ObservableCollection<MenuItemModel>();
        public ObservableCollection<MenuItemModel> Menus
        {
            get { return _menus; }
            set { SetProperty(ref _menus, value); }
        }

        /// <summary>
        /// 递归查找子菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parent_id"></param>
        private void FillMenus(ObservableCollection<MenuItemModel> menus,
           int parent_id)
        {
            var sub = sysMenuList.Where(m => m.ParentId == parent_id)
                .OrderBy(o => o.Index)
                .ToList();

            if (sub.Count() > 0)
            {
                foreach (Entities.SysMenu item in sub)
                {
                    MenuItemModel model = new MenuItemModel
                    {
                        MenuHeader = item.MenuHeader,
                        MenuIcon = item.MenuIcon,
                        TargetView = item.TargetView
                    };
                    menus.Add(model);

                    FillMenus(model.Children, item.MenuId);
                }
            }
        }
        private void DoOpenView(MenuItemModel itemModel)
        {
            // 需要判断：双击的是父节点的时候，关闭或者打开；双击的是子节点，打开对应的页面
            if (itemModel.Children != null && itemModel.Children.Count > 0)
            {
                itemModel.IsExpanded = !itemModel.IsExpanded;

            }
            else if (!string.IsNullOrEmpty(itemModel.TargetView))
            {
                _regionManager.RequestNavigate("MainRegion", itemModel.TargetView);
            }
           
        }
        #endregion
    }
}
