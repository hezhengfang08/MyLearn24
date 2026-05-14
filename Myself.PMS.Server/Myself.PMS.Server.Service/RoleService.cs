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
        public bool CheckRoleName(string roleName, int id)
        {
            return _client.Queryable<SysRole>()
                .Any(r => r.RoleName == roleName &&
                        r.RoleId != id);
        }
        public int DeleteRole(int id)
        {
            // 删除RoleMenu、RoleUser
            int count = 0;
            try
            {
                _client.Ado.BeginTran();
                _client.Deleteable<RoleMenu>().Where(rm => rm.RoleId == id).ExecuteCommand();
                _client.Deleteable<RoleUser>().Where(ru => ru.RoleId == id).ExecuteCommand();
                count = _client.Deleteable<SysRole>().In(id).ExecuteCommand();
                _client.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _client.Ado.RollbackTran();
                throw ex;
            }
            return count;
        }
        public SysRole[] GetAllRoles(string key)
        {
            return _client.Queryable<SysRole>()
                .Includes(r => r.Menus.MappingField(m => m.RoleId, () => r.RoleId).ToList())
                .Includes(r => r.Users.MappingField(u => u.RoleId, () => r.RoleId).ToList())
                .Where(r =>
                    string.IsNullOrEmpty(key) ||
                    r.RoleName.Contains(key) ||
                    r.RoleDesc.Contains(key)).ToArray();
        }

        public int Update(SysRole role)
        {
            int count = 0;
            if (role.RoleId == 0)
            {
                count = _client.Insertable(role).IgnoreColumns(r => r.RoleId).ExecuteCommand();
            }
            else
            {
                count = _client.Updateable(role).ExecuteCommand();
            }

            return count;
        }
        public SysRole[] GetRoleByIds(int[] id)
        {
            return _client.Queryable<SysRole>()
                .Where(r => id.Contains(r.RoleId))
                .ToArray();
        }

        public int UpdateRoleMenus(RoleMenu[] rms)
        {
            _client.Deleteable<RoleMenu>()
               .Where(rm => rm.RoleId == rms[0].RoleId)
               .ExecuteCommand();

            return _client.Insertable(rms).ExecuteCommand();
        }

        public int UpdateRoleUsers(RoleUser[] users)
        {
            _client.Deleteable<RoleUser>()
               .Where(ru => ru.RoleId == users[0].RoleId)
               .ExecuteCommand();

            return _client.Insertable(users).ExecuteCommand();
        }

        public int DeleteRoleUser(int rid, int uid)
        {
            return _client.Deleteable<RoleUser>()
                .Where(ru => ru.RoleId == rid && ru.UserId == uid)
                .ExecuteCommand();
        }
    }
}
