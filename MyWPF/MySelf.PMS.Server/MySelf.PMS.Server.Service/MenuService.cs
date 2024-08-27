using MySelf.PMS.Server.Entities;
using MySelf.PMS.Server.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Server.Service
{
    public class MenuService : IMenuService
    {
        ISqlSugarClient _client;
        public MenuService(ISqlSugarClient client)
        {
            _client = client;
        }

       

        public IEnumerable<MenuEntity> GetAllMenus(string key)
        {
            // 条件 ：数据库中的记录，State>=0   ,
            //       并且

            //       （Key是空的情况，返回所有记录，
            //              或者 MenuHeader匹配这个关键词，
            //              或者 TargetView匹配这个关键词，
            //              或者 当前菜单的子菜单相关的字段（MenuHeader/TargetView）也能匹配穿上关键词）
            var ms = _client.Queryable<MenuEntity>()
                .Where(m => m.State >= 0 &&

                (string.IsNullOrEmpty(key) ||
                    (
                      m.MenuHeader.Contains(key) ||
                      m.TargetView.Contains(key) ||

                      SqlFunc.Subqueryable<MenuEntity>().Where(sm => sm.ParentId == m.MenuId &&
                                            (sm.MenuHeader.Contains(key) || sm.TargetView.Contains(key))).Count() > 0
                    ))
                )
                .ToList();

            return ms;
        }

       

        public int Update(MenuEntity menu)
        {
            int count = 0;
            if (string.IsNullOrEmpty(menu.MenuId))
            {
                //新增的逻辑
                var max = _client.Queryable<MenuEntity>()
                    .Where(m=>m.ParentId == menu.ParentId)
                    .Max(m => m.MenuId);
                menu.MenuId = (int.Parse(max.ToString())+1).ToString();
                count = _client.Insertable(menu).ExecuteCommand();  
            }
            else
            {
                var menuEntity = _client.Queryable<MenuEntity>()
                    .Where(m=>m.MenuId == menu.MenuId)
                    .ToList().FirstOrDefault();
                if (menuEntity == null)
                {
                    throw new Exception("没有匹配的菜单的信息");
                }
                menuEntity.MenuHeader = menu.MenuHeader;    
                menuEntity.ParentId = menu.ParentId;
                menuEntity.TargetView = menu.TargetView;
                menuEntity.MenuIcon = menu.MenuIcon;
                count = _client.Updateable(menuEntity).ExecuteCommand();    
            }
            return count;   
        }
        public int Delete(string id)
        {
            return _client.Deleteable<MenuEntity>().In(id).ExecuteCommand();
        }
    }
}
