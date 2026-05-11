using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Service
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
    }
}
