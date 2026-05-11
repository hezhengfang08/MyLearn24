using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.BLL
{
    public class MenuService : IMenuService
    {
        IMenuAccess _menuAccess;
        public MenuService(IMenuAccess menuAccess)
        {
            _menuAccess = menuAccess;
        }
        public IEnumerable<MenuEntity> GetAllMenus(string key)
        {
            string json = _menuAccess.GetAllMenus(key);
            var result = json.Deserialize<Result<MenuEntity[]>>();
            if (result == null)
                throw new Exception("菜单数据获取失败!");
            if (result.State != 200)
                throw new Exception(result.ExceptionMessage);

            return result.Data;
        }
    }
}
