using MySelf.PMS.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.IBLL
{
    public interface IMenuService
    {
        IEnumerable<MenuEntity> GetAllMenus(string key="");
        int UpdateMenu(MenuEntity menu);
        int DeleteMenu(string id);
    }
}
