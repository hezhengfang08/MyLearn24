using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IBLL;
using MySelf.PMS.Client.IDAL;
using MySelf.PMS.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Client.BLL
{
    public class MenuService : IMenuService
    {
        private IMenuAccess _menuAccess;
        public MenuService(IMenuAccess menuAccess) {
         _menuAccess = menuAccess;
        }    
        public IEnumerable<MenuEntity> GetAllMenus()
        {
            string json = _menuAccess.GetAllMenus();
            var result = JsonUtil.Deserializer<ResultEntiy<MenuEntity[]>>(json);
            if (result == null)
                throw new Exception("菜单数据获取失败!");
            if (result.RCode != ResultCode.Success)
                throw new Exception(result.Message);

            return result.Data;
        }
    }
}
