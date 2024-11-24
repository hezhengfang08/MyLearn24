using Myself.SmartParing.IService;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Myself.SmartParking.ViewModels
{
    public class MainViewModel:BindableBase
    {
        List<Entities.SysMenu> sysMenuList;
        public MainViewModel(IDialogService dialogService ,IMenuService menuService) {
            // 打开登录窗口
            dialogService.ShowDialog("LoginView", rerult =>
            {
                if (rerult.Result != ButtonResult.OK)
                {
                    System.Environment.Exit(0);
                }
            });
            // 当前窗口要做的事
            // 加载菜单
            sysMenuList = menuService.GetMenuList().ToList();
            FillMenus(Menus, 0);
        }

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
    }
}
