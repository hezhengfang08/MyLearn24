using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.SmartParking.ViewModels.Pages.Dialogs
{
    public class ModifyMenuViewModel : DialogViewModelBase
    {
        private readonly IMenuService _menuService;
        public ModifyMenuViewModel(IMenuService menuService)
        {
            _menuService = menuService;
           
        }

        // 显式实现接口属性
        public DialogCloseListener RequestClose { get; private set; }

        public MenuItemModel MenuModel { get; set; } =
                new MenuItemModel();

        public List<SysMenu> ParentNodes { get; set; } =
          new List<SysMenu>();
        public override  void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<MenuItemModel>("model");
            if (model == null)
            {
                Title = "新增菜单项";

                MenuModel.ParentId = 0;// 默认新增一个一级菜单
                MenuModel.MenuType = 1;// 默认新增一个集合类型的节点
            }
            else
            {
                Title = "编辑菜单项";

                var sm = _menuService.Find<SysMenu>(model.MenuId);
                MenuModel.MenuId = sm.MenuId;
                MenuModel.MenuHeader = sm.MenuHeader;
                MenuModel.ParentId = sm.ParentId;
                MenuModel.TargetView = sm.TargetView;
                MenuModel.MenuIcon = sm.MenuIcon;
                MenuModel.MenuType = sm.MenuType;

                //SelectedParentId = sm.ParentId;
            }

            ParentNodes = _menuService.Query<SysMenu>(m => m.MenuType == 1)
                                      .ToList();
            ParentNodes.Insert(0, new SysMenu { MenuHeader = "ROOT", MenuId = 0 });

            /// 1、将model直接作为数据源，提供给页面显示。
            ///    问题：子窗口修改的每一个动作，都会反馈到 主窗口
            ///    
            /// 2、根据model中的MenuId进行数据检索，后进行显示
            ///    数据更新完成后，提交到了数据库，需要主窗口进行同步操作
            ///    
        }

        public override void DoSave()
        {
            try
            {
                if (MenuModel.MenuId == 0)
                {
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    uint key = Convert.ToUInt32(ts.TotalSeconds);
                    // 新增
                    _menuService.Insert<SysMenu>(new SysMenu
                    {
                        MenuId = (int)key,// 新增Id
                        MenuHeader = MenuModel.MenuHeader,
                        MenuIcon = MenuModel.MenuIcon,
                        TargetView = MenuModel.TargetView,
                        ParentId = MenuModel.ParentId,
                        MenuType = MenuModel.MenuType,
                        State = 1
                    });
                }
                else
                {
                    // 编辑
                    var entity = _menuService.Find<SysMenu>(MenuModel.MenuId);
                    entity.MenuHeader = MenuModel.MenuHeader;
                    entity.MenuIcon = MenuModel.MenuIcon;
                    entity.TargetView = MenuModel.TargetView;
                    entity.ParentId = MenuModel.ParentId;
                    entity.MenuType = MenuModel.MenuType;
                    _menuService.Update<SysMenu>(entity);
                }
               base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
