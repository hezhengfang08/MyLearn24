using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{
    public class MenuListViewModel:ListVMBase
    {
        bool oldShowDel = false;
        IMenuService menuService = InstanceFactory.CreateInstance<IMenuService>();
        public MenuListViewModel()
        {

        }
        #region 属性
        private ObservableCollection<MenuModel> menuList;

        public ObservableCollection<MenuModel> MenuList
        {
            get { return menuList; }
            set { menuList = value; 
            OnPropertyChanged();
            }
        }

        #endregion
        #region 方法


        //显示菜单信息窗口
        private void ShowMenuWindow(int actType, ViewMenuInfo menuInfo)
        {
            MenuInfoViewModel menuVM = new MenuInfoViewModel(actType, menuInfo);
            //注册刷新事件
            menuVM.ReloadList += MenuVM_ReloadList;
            ShowDialog("menuWindow", menuVM);
        }

        private void MenuVM_ReloadList(object sender, InfoArgs<ViewMenuInfo> e)
        {
            if (e != null)
            {
                if (e.ActType == 1 && MenuList.Count < PageInfoVM.PageSize)
                {
                    MenuList.Add(new MenuModel(e.Info));
                }
                else if (e.ActType == 2)
                {
                    MenuList.Where(m => m.Menu.MenuId == e.Info.MenuId).First().Menu = e.Info;//替换
                }
            }
        }

        private void FindMenuList()
        {
            ObservableCollection<MenuModel> list = new ObservableCollection<MenuModel>();
            //如果改变了删除状态选择，重新从第一页开始呈现
            if (HasDeleted != oldShowDel)
            {
                PageInfoVM.CurrentPage = 1;
                oldShowDel = HasDeleted;
            }
            PageModel<ViewMenuInfo> result = menuService.GetMenuList(KeyWords, HasDeleted, PageInfoVM.StartIndex, PageInfoVM.PageSize);
            if (result != null)
            {
                List<ViewMenuInfo> menuData = null;
                if(result.DataList.Count > 0)
                {
                    menuData = result.DataList;
                    PageInfoVM.TotalCount = result.TotalCount;
                    menuData.ForEach(item=>list.Add(new MenuModel(item)));  
                }
                else
                {
                    PageInfoVM.TotalCount = 0;
                }
            }
            MenuList = list;
        }
        private void DeleteMenu(MenuModel model ,int delType)
        {
            string info = "菜单";
            //操作名称
            string delName = CommonHelper.GetDelTypeName(delType);
            string msgTitle = $"{delName}{info}";
            if (ShowQuestion($"你确定要{delName}该{info}吗？", msgTitle) == MessageBoxResult.OK) { 
                ViewMenuInfo menu = model.Menu;
                bool res = false;
                switch(delType)
                {
                    case 1://逻辑删除
                        if (menuService.IsHasChildMenu(menu.MenuId))
                        {
                            ShowError("该菜单下已有添加了子菜单，不能删除！", msgTitle);
                            return;
                        }
                        res = menuService.DeleteMenu(menu.MenuId);
                        break;
                    case 2://恢复 
                        if (menuService.ExistMenu(menu.MenuName))
                        {
                            ShowError("已存在与该名称相同的菜单，不能恢复！", msgTitle);
                            return;
                        }
                        res = menuService.RecoveryMenu(menu.MenuId);
                        break;
                    case 3://真删除
                        res = menuService.RemoveMenu(menu.MenuId);
                        break;
                    default: break;
                }
                if (res)
                {
                    ShowSuccess($"该{info}{delName}成功！", msgTitle);
                    //刷新
                    MenuList.Remove(model);
                }
                else
                {
                    ShowError($"该{info}{delName}失败！", msgTitle);
                    return;
                }
            }

        }
        //命令初始化
        private void InitCommands()
        {
            AddItemCmd = new RelayCommand(o =>
            {
                //打开菜单信息窗口
                ShowMenuWindow(1, null);
            });

            EditItemCmd = new RelayCommand(m =>
            {
                ViewMenuInfo menuInfo = m as ViewMenuInfo;
                ShowMenuWindow(2, menuInfo);
            });

            FindDataListCmd = new RelayCommand(o =>
            {
                FindMenuList();
            });

            DelItemCmd = new RelayCommand(m =>
            {
                MenuModel model = m as MenuModel;
                DeleteMenu(model, 1);
            });
            RecoverItemCmd = new RelayCommand(m =>
            {
                MenuModel model = m as MenuModel;
                DeleteMenu(model, 2);
            });
            RemoveItemCmd = new RelayCommand(m =>
            {
                MenuModel model = m as MenuModel;
                DeleteMenu(model, 3);
            });
        }
        #endregion
    }
}
