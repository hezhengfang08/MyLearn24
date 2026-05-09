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
        public IEnumerable<MenuEntity> GetAllMenus()
        {
            var ms = _client.Queryable<MenuEntity>()
                .Where(m => m.State >= 0)
                .ToList();

            return ms;
        }
    }
}
