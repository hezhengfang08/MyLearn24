using MySelf.PMS.Client.MainModule.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.MainModule.ViewModels
{
    public class PageViewModel : INavigationAware
    {
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 导航进来的参数值   --  菜单数据
            var sub_menus = navigationContext.Parameters
                .GetValue<List<Entities.MenuEntity>>("menu");
            // 
            Menus.Clear();
            // 提供一个集合进行数据的显示
            foreach (var menu in sub_menus)
            {
                string icon = ((char)int.Parse(menu.MenuIcon, NumberStyles.HexNumber))
                    .ToString();
                Menus.Add(new MenuModel
                {
                    MenuHeader = menu.MenuHeader,
                    TargetView = menu.TargetView,
                    MenuIcon = icon,
                });
            }
        }
        public ObservableCollection<MenuModel> Menus { get; set; } =
            new ObservableCollection<MenuModel>();

        public DelegateCommand<string> MenuCommand { get; set; }

        IRegionManager _regionManager;
        public PageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            MenuCommand = new DelegateCommand<string>(DoMenu);
        }
        private void DoMenu(string page)
        {
            _regionManager.RequestNavigate("PageRegion", page);
        }
    }
}
