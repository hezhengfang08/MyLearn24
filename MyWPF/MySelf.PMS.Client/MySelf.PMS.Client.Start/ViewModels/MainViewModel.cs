using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.Start.Models;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;

using System.Windows;

namespace MySelf.PMS.Client.Start.ViewModels
{
    public class MainViewModel
    {
        public List<MenuModel> Menus { get; set; } = new List<MenuModel>();
        private Entities.MenuEntity[] menus;

        public DelegateCommand<string> PageSwitchCommand { get; set; }
        public MainViewModel(IDialogService dialogService, IMenuService menuService)
        {
            // 打开登录弹窗
            dialogService.ShowDialog("LoginView", result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    Application.Current.Shutdown();
                }
                var user = result.Parameters.GetValue<Entities.EmployeeEntity>("user");
            });
            PageSwitchCommand = new DelegateCommand<string>(DoPageSwitch);
            // 获取出所需要的第一级菜单信息
            menus = menuService.GetAllMenus().ToArray();
            foreach (var me in menus.Where(m => m.ParentId == "0"))
            {
                Menus.Add(new MenuModel { MenuId = me.MenuId, MenuHeader = me.MenuHeader });
            }
        }

        private void DoPageSwitch(string id)
        {
            //to do
        }
    }
}
