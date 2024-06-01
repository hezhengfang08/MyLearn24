using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{


    public interface IMenuService
    {
        List<UIMenu> GetRoleMenuList(int roleId, int parentId);
        PageModel<ViewMenuInfo> GetMenuList(string keywords, bool showDel, int startIndex, int pageSize);
        List<CboMenu> GetCboMenus();
        bool DeleteMenu(int menuId);
        bool RecoveryMenu(int menuId);  
        bool RemoveMenu(int menuId);
        bool ExistMenu(string  menuName);
        bool SaveMenu(MenuInfo menuInfo);
        bool IsHasChildMenu(int menuId);
        List<int> GetRoleMenuIds(int roleId);


    }
}
