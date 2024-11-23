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
        public IEnumerable<MenuEntity> GetAllMenus(string key = "")
        {
            string json = _menuAccess.GetAllMenus(key);
            if (!string.IsNullOrEmpty(json))
            {
                var result = JsonUtil.Deserializer<ResultEntiy<MenuEntity[]>>(json);
                if (result == null)
                    throw new Exception("菜单数据获取失败!");
                if (result.State != ResultCode.Success)
                    throw new Exception(result.Message);

                return result.Data;
            }
            return null;

        }

        public int UpdateMenu(MenuEntity menu)
        {
            string menu_json = System.Text.Json.JsonSerializer.Serialize(menu);
            string json = _menuAccess.UpdateMenu(menu_json);
            var result = JsonUtil.Deserializer<ResultEntiy<int>>(json);
            if (result == null)
                throw new Exception("菜单数据获取失败!");
            if (result.State != ResultCode.Success)
                throw new Exception(result.Message);
            if (result.Data == 0)
                throw new Exception("未更新任何数据");
            return result.Data;
        }
        public int DeleteMenu(string id)
        {
            string json = _menuAccess.DeleteMenu(id);
            var result = JsonUtil.Deserializer<ResultEntiy<int>>(json);

            if (result == null)
                throw new Exception("菜单数据获取失败!");
            if (result.State != ResultCode.Success)
                throw new Exception(result.Message);
            if (result.Data == 0)
                throw new Exception("未更新任何数据");

            return result.Data;
        }
    }
}
