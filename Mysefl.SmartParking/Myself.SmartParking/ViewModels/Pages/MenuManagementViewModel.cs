using Myself.SmartParing.IService;
using Myself.SmartParking.Base;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class MenuManagementViewModel : ViewModelBase<MenuItemModel>
    {
        private readonly IMenuService _menuService;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        public MenuManagementViewModel(
            IRegionManager regionManager,
            IMenuService menuService,
            IDialogService dialogService,
            IEventAggregator eventAggregator)
            : base(regionManager)
        {
            //_regionManager = regionManager;
            //CloseCommand = new DelegateCommand(DoClose);
            PageTitle = "菜单数据维护";
            _menuService = menuService;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            Refresh();
        }
    
        public ObservableCollection<MenuItemModel> Menus { get; set; } = new ObservableCollection<MenuItemModel>();
        List<Entities.SysMenu> sysMenuList;
        string _searchKey;
        public override void Refresh()
        {
            
            Menus.Clear();
            sysMenuList = _menuService.GetMenuList(SearchKey).ToList();
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
                        MenuId = menu.MenuId,
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
        public override void DoModify(MenuItemModel model)
        {
            DialogParameters ps = new DialogParameters();
            ps.Add("model", model);
            _dialogService.ShowDialog("ModifyMenuView", ps, result =>
            {
                // 判断子窗口的返回状态，如果OK，刷新当前页面，否则不管
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                    PublishRefreshMenuEvent();


                }
            });
        }
        private void PublishRefreshMenuEvent()
        {
            _eventAggregator.GetEvent<RefreshMenuEvent>()
                        .Publish();
        }
        public override void DoDelete(MenuItemModel model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    // 物理删除
                    _menuService.Delete<SysMenu>(model.MenuId);

                    // 逻辑删除
                    // 通过特定字段进行数据过滤
                    MessageBox.Show("删除完成！", "提示");

                    this.Refresh();

                    PublishRefreshMenuEvent();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
