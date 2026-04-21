using Myself.SmartParing.IService;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class MenuManagementViewModel : ViewModelBase
    {
        //public string PageTitle { get; set; } = "菜单数据维护";
        //public bool IsCanClose { get; set; } = true;

        //public DelegateCommand CloseCommand { get; set; }

        //IRegionManager _regionManager;
        IMenuService _menuService;
        public MenuManagementViewModel(
            IRegionManager regionManager
            , IMenuService menuService) : base(regionManager)
        {
            //_regionManager = regionManager;
            //CloseCommand = new DelegateCommand(DoClose);
            PageTitle = "菜单数据维护";
            _menuService = menuService;
            Refresh();
        }
        //private void DoClose()
        //{
        //    //拿到主区域，从区域里移除对应的页面，根据页面的名称
        //    var region = _regionManager.Regions["MainRegion"];
        //    var view = region.Views.Where(v => v.GetType().Name == "MenuManagementView").FirstOrDefault();
        //    if (view != null)
        //    {
        //        region.Remove(view);
        //    }
        //}
        public ObservableCollection<MenuItemModel> Menus { get; set; } = new ObservableCollection<MenuItemModel>();
        List<Entities.SysMenu> sysMenuList;
        private void Refresh()
        {
            sysMenuList = _menuService.GetMenuList().ToList();
            FillMenus(Menus, 0);
        }
        private void FillMenus(ObservableCollection<MenuItemModel> menuList, int parent_id)
        {
            var sub = sysMenuList.Where(m=>m.ParentId == parent_id).OrderBy(m=>m.Index).ToList();
            if (sub.Count > 0) {
                foreach (var menu in sub) {

                    MenuItemModel menuItem = new MenuItemModel()
                    {
                        MenuHeader = menu.MenuHeader,
                        MenuIcon = menu.MenuIcon,
                        TargetView = menu.TargetView,
                        ParentId = parent_id,
                        IsExpanded = true
                    };
                    menuList.Add(menuItem);
                    FillMenus(menuItem.Children, menu.MenuId);
                }
                if(parent_id >0)
                {
                    menuList[menuList.Count-1].IsLastChild = true;  
                }
            }
        }
    }
}
