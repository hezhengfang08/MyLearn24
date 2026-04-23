using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Base
{
    public class MenuHelper
    {
        public static void FillMenus(ObservableCollection<MenuItemModel> menus,
            MenuItemModel parent, List<Entities.SysMenu> origMenus, bool expandedNode = true)
        {
            var sub = origMenus.Where(m => m.ParentId == (parent == null ? 0 : parent.MenuId))
                .OrderBy(o => o.Index)
                .ToList();

            if (sub.Count() > 0)
            {
                foreach (Entities.SysMenu item in sub)
                {
                    MenuItemModel model = new MenuItemModel
                    {
                        MenuId = item.MenuId,
                        MenuHeader = item.MenuHeader,
                        MenuIcon = item.MenuIcon,
                        TargetView = item.TargetView,
                        ParentId = (parent == null ? 0 : parent.MenuId),
                        IsExpanded = expandedNode,
                        Parent = parent
                    };
                    menus.Add(model);

                    FillMenus(model.Children, model, origMenus);
                }

                if (menus.Count > 0)
                {
                    menus[menus.Count - 1].IsLastChild = true;
                }
            }
        }
    }
}
