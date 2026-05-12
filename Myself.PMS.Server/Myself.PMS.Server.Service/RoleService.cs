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
    public class RoleService : IRoleService
    {
        ISqlSugarClient _client;
        public RoleService(ISqlSugarClient client)
        {
            _client = client;
        }
        public SysRole[] GetRoleByIds(int[] id)
        {
            return _client.Queryable<SysRole>()
                .Where(r => id.Contains(r.RoleId))
                .ToArray();
        }
    }
}
